using App.Gameplay.Resource;
using Atomic;
using UnityEngine;

namespace App.Gameplay
{
    public class DetectionResourceMechanics
    {
        private readonly IAtomicVariable<bool> _canGathering;
        
        private readonly IAtomicValue<ResourceModel> _targetResource;
        private readonly IAtomicValue<Vector3> _moveDirection;
        private readonly IAtomicValue<float> _gatheringDistance;
        private readonly IAtomicValue<bool> _isFreeSpace; 
        private readonly Transform _root;
        
        public DetectionResourceMechanics(
            Transform root, 
            IAtomicValue<ResourceModel> targetResource,
            IAtomicValue<float> gatheringDistance,
            IAtomicVariable<bool> canGathering, 
            IAtomicValue<bool> isFreeSpace,
            IAtomicValue<Vector3> moveDirection)
        {
            _root = root;
            _targetResource = targetResource;
            _gatheringDistance = gatheringDistance;
            _canGathering = canGathering;
            _isFreeSpace = isFreeSpace;
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

            if (!_isFreeSpace.Value)
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