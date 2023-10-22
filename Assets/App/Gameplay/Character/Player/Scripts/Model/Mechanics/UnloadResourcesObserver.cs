using App.Gameplay.LevelStorage;
using Atomic;
using UnityEngine;

namespace App.Gameplay
{
    public class UnloadResourcesObserver
    {
        private readonly AtomicVariable<Vector3> _moveDirection;
        private readonly AtomicVariable<bool> _canUnloadResources;
        private readonly AtomicVariable<LevelStorageModel> _levelStorageModel;

        public UnloadResourcesObserver(
            AtomicVariable<Vector3> moveDirection, 
            AtomicVariable<bool> canUnloadResources, 
            AtomicVariable<LevelStorageModel> levelStorageModel)
        {
            _moveDirection = moveDirection;
            _canUnloadResources = canUnloadResources;
            _levelStorageModel = levelStorageModel;
        }

        public void Update()
        {
            if (_moveDirection.Value == Vector3.zero)
            {
                if (_levelStorageModel.Value != null)
                {
                    _canUnloadResources.Value = true;
                    return;
                }
            }

            _canUnloadResources.Value = false;
        }
    }
}