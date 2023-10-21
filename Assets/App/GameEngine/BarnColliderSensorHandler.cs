using App.Gameplay.LevelStorage;
using Atomic;
using UnityEngine;

namespace App.Gameplay
{
    public class BarnColliderSensorHandler : IColliderSensorHandler
    {
        public AtomicVariable<LevelStorageModel> LevelStorageModel;

        public void OnColliderUpdated(Collider[] colliders)
        {
            foreach (var collider in colliders)
            {
                if (collider.TryGetComponent<LevelStorageModel>(out var levelStorageModel))
                {
                    LevelStorageModel.Value = levelStorageModel;
                    break;
                }
            }
        }
    }
}