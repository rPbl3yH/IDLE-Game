﻿using App.Gameplay.LevelStorage;
using App.Gameplay.ResourceStorage;
using Modules.Atomic.Values;

namespace App.Gameplay.Character.Scripts.Model.Mechanics
{
    public class UnloadResourcesMechanics
    {
        private readonly AtomicVariable<BarnModel> _levelStorageModel;
        private readonly AtomicVariable<bool> _canUnloadResources;
        private readonly AtomicVariable<float> _delay;
        private readonly AtomicVariable<ResourceType> _resourceType;
        private readonly AtomicVariable<int> _amount;

        private float _timer;
        
        public UnloadResourcesMechanics(
            AtomicVariable<BarnModel> levelStorageModel,
            AtomicVariable<bool> canUnloadResources,
            AtomicVariable<float> delay,
            AtomicVariable<ResourceType> resourceType,
            AtomicVariable<int> amount)
        {
            _levelStorageModel = levelStorageModel;
            _canUnloadResources = canUnloadResources;
            _delay = delay;
            _resourceType = resourceType;
            _amount = amount;
        }

        public UnloadResourcesMechanics(CharacterModel characterModel)
        {
            _levelStorageModel = characterModel.LevelStorage;
            _canUnloadResources = characterModel.CanUnloadResources;
            _delay = characterModel.Delay;
            _resourceType = characterModel.ResourceType;
            _amount = characterModel.Amount;
        }

        public void Update(float deltaTime)
        {
            if (!_canUnloadResources.Value)
            {
                return;
            }
            
            _timer += deltaTime;
            
            if (_timer >= _delay.Value)
            {
                ResetTimer();

                if (_amount.Value == 0)
                {
                    return;
                }

                var unloadCount = 1;
                var resourceData = new ResourceData(_resourceType.Value, unloadCount);
                _levelStorageModel.Value.ResourceAdded?.Invoke(resourceData);
                _amount.Value -= unloadCount;
            }
        }

        private void ResetTimer()
        {
            _timer = 0f;
        }
    }
}