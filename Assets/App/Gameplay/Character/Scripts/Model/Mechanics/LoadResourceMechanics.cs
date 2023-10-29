using Modules.Atomic.Actions;
using Modules.Atomic.Values;

namespace App.Gameplay.Character.Scripts.Model.Mechanics
{
    public class LoadResourceMechanics
    {
        private readonly AtomicEvent<ResourceType> _resourceLoaded;
        private readonly AtomicVariable<ResourceType> _transferResourceType;
        private readonly AtomicVariable<bool> _isFreeSpace;
        private readonly AtomicVariable<bool> _canTransferResources;
        private readonly AtomicVariable<bool> _canLoadResources;
        private readonly AtomicVariable<float> _loadDelay;

        private float _timer;

        public LoadResourceMechanics(CharacterModel characterModel)
        {
            _resourceLoaded = characterModel.ResourceLoaded;
            _transferResourceType = characterModel.LoadResourceType;
            _isFreeSpace = characterModel.IsFreeSpace;
            _canTransferResources = characterModel.CanTransferResources;
            _canLoadResources = characterModel.CanLoadResources;
            _loadDelay = characterModel.Delay;
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

                if (_isFreeSpace.Value)
                {
                    _resourceLoaded?.Invoke(_transferResourceType.Value);
                }
            }
        }
        
        
        private void OnResourceLoaded(ResourceType obj)
        {
            // _characterModel.ResourceType.Value = obj;
            // _characterModel.Amount.Value++;
        }
        
        private void ResetTimer()
        {
            _timer = 0f;
        }
    }
}