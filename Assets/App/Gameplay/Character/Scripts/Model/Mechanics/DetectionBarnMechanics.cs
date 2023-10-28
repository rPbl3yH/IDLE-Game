using System.Linq;
using App.GameEngine;
using App.Gameplay.LevelStorage;
using Modules.Atomic.Values;
using UnityEngine;

namespace App.Gameplay.Character.Scripts.Model.Mechanics
{
    public class DetectionBarnMechanics
    {
        private readonly AtomicVariable<ResourceStorageModel> _levelStorageModel;
        private readonly ColliderSensor _colliderSensor;
        
        public DetectionBarnMechanics(AtomicVariable<ResourceStorageModel> levelStorageModel, ColliderSensor colliderSensor)
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
            var value = colliders.FirstOrDefault()?.GetComponent<ResourceStorageModel>();
            _levelStorageModel.Value = value;
        }
    }
}