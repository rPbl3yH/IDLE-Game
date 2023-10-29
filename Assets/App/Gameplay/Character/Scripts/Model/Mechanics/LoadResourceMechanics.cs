using App.Gameplay.LevelStorage;
using Modules.Atomic.Actions;
using Modules.Atomic.Values;
using UnityEngine;

namespace App.Gameplay.Character.Scripts.Model.Mechanics
{
    public class LoadResourceMechanics
    {
        private readonly AtomicEvent<ResourceType> _resourceLoaded;
        private readonly AtomicVariable<ResourceType> _resourceType;
        private readonly AtomicVariable<ResourceType> _loadResourceType;
        private readonly AtomicVariable<bool> _isFreeSpace;
        private readonly AtomicVariable<int> _amount;
        private readonly AtomicVariable<bool> _canLoadResources;
        private readonly AtomicVariable<float> _loadDelay;
        private readonly AtomicVariable<ResourceStorageModel> _resourceStorage;

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
                
                Debug.Log("Resource loaded");
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