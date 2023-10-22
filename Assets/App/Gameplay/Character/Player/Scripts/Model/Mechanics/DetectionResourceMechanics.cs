using App.Gameplay.Resource;
using Atomic;
using UnityEngine;

namespace App.Gameplay
{
    public class DetectionResourceMechanics
    {
        private readonly AtomicVariable<ResourceModel> _targetResource;
        private readonly AtomicVariable<bool> _canGathering;
        private readonly AtomicVariable<Vector3> _moveDirection;
        private readonly AtomicVariable<float> _gatheringDistance;
        private readonly Transform _root;
        
        public DetectionResourceMechanics(
            Transform root, 
            AtomicVariable<ResourceModel> targetResource,
            AtomicVariable<float> gatheringDistance,
            AtomicVariable<bool> canGathering, 
            AtomicVariable<Vector3> moveDirection)
        {
            _root = root;
            _targetResource = targetResource;
            _gatheringDistance = gatheringDistance;
            _canGathering = canGathering;
            _moveDirection = moveDirection;
        }

        public void Update()
        {
            if (_moveDirection.Value != Vector3.zero)
            {
                _canGathering.Value = false;
                return;
            }

            if (_targetResource.Value == null)
            {
                _canGathering.Value = false;
                return;
            }

            var delta = _targetResource.Value.transform.position - _root.transform.position;
            var distance = delta.magnitude;

            if (distance <= _gatheringDistance.Value)
            {
                _canGathering.Value = true;
            }
            else
            {
                _canGathering.Value = false;
            }
        }
    }
}