using System.Linq;
using App.Gameplay.LevelStorage;
using Atomic;
using UnityEngine;

namespace App.Gameplay
{
    public class DetectionBarnMechanics
    {
        private readonly AtomicVariable<LevelStorageModel> _levelStorageModel;
        private readonly ColliderSensor _colliderSensor;
        
        public DetectionBarnMechanics(AtomicVariable<LevelStorageModel> levelStorageModel, ColliderSensor colliderSensor)
        {
            _levelStorageModel = levelStorageModel;
            _colliderSensor = colliderSensor;
        }

        public void OnEnable()
        {
            _colliderSensor.ColliderUpdated += OnColliderUpdated;
        }

        public void OnDisable()
        {
            _colliderSensor.ColliderUpdated -= OnColliderUpdated;
        }

        private void OnColliderUpdated(Collider[] colliders)
        {
            var value = colliders.FirstOrDefault()?.GetComponent<LevelStorageModel>();
            _levelStorageModel.Value = value;
        }
    }
}