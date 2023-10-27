using App.Gameplay.LevelStorage;
using Atomic;
using UnityEngine;

namespace App.Gameplay
{
    public class BarnColliderSensorHandler : IColliderSensorHandler
    {
        public AtomicVariable<BarnModel> LevelStorageModel;

        public void OnColliderUpdated(Collider[] colliders)
        {
            foreach (var collider in colliders)
            {
                if (collider.TryGetComponent<BarnModel>(out var levelStorageModel))
                {
                    LevelStorageModel.Value = levelStorageModel;
                    break;
                }
            }
        }
    }
}