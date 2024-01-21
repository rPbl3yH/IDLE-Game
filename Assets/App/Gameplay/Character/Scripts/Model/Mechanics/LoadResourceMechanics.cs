using App.Gameplay.LevelStorage;
using Atomic.Elements;

namespace App.Gameplay.Character.Scripts.Model.Mechanics
{
    public class LoadResourceMechanics
    {
        private readonly AtomicEvent<ResourceType> _resourceLoaded;
        private readonly IAtomicVariable<ResourceType> _resourceType;
        private readonly IAtomicValue<ResourceType> _loadResourceType;
        private readonly IAtomicValue<bool> _isFreeSpace;
        private readonly IAtomicVariable<int> _amount;
        private readonly IAtomicVariable<bool> _canLoadResources;
        private readonly IAtomicValue<float> _loadDelay;
        private readonly IAtomicValue<ResourceStorageModel> _resourceStorage;

        private float _timer;

        public LoadResourceMechanics(CharacterModel characterModel)
        {
            _resourceLoaded = characterModel.ResourceLoaded;
            _loadResourceType = characterModel.LoadResourceType;
            _isFreeSpace = characterModel.IsFreeSpace;
            _resourceType = characterModel.ResourceType;
            _amount = characterModel.ResourceAmount;
            _canLoadResources = characterModel.CanLoadResources;
            _loadDelay = characterModel.Delay;
            _resourceStorage = characterModel.ResourceStorage;
        }
        
        public void Update(float deltaTime)
        {
            if (!_canLoadResources.Value)
            {
                return;
            }
            
            _timer += deltaTime;
            
            if (_timer >= _loadDelay.Value)
            {
                ResetTimer();

                if (!_isFreeSpace.Value)
                {
                    return;
                }
                
                if(!_resourceStorage.Value.ResourceStorage.TryRemove(_loadResourceType.Value, 1))
                {
                    _canLoadResources.Value = false;
                    return;
                }
                
                _resourceType.Value = _loadResourceType.Value;
                _amount.Value++;
                _resourceLoaded?.Invoke(_loadResourceType.Value);
            }
        }

        private void ResetTimer()
        {
            _timer = 0f;
        }
    }
}