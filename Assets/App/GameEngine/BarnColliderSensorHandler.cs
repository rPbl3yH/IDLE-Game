using App.Gameplay.LevelStorage;
using Modules.Atomic.Values;
using UnityEngine;

namespace App.GameEngine
{
    public class BarnColliderSensorHandler : IColliderSensorHandler
    {
        public AtomicVariable<ResourceStorageModel> LevelStorageModel;

        public void OnColliderUpdated(Collider[] colliders)
        {
            foreach (var collider in colliders)
            {
                if (collider.TryGetComponent<ResourceStorageModel>(out var levelStorageModel))
                {
                    LevelStorageModel.Value = levelStorageModel;
                    break;
                }
            }
        }
    }
}