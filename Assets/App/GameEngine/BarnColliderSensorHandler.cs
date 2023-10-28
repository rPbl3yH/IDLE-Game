using App.Gameplay.LevelStorage;
using Modules.Atomic.Values;
using UnityEngine;

namespace App.GameEngine
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