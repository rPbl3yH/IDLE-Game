using System.Collections.Generic;
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
        private readonly ColliderSensor _colliderSensor;
        private readonly Transform _root;

        public DetectionResourceMechanics(
            Transform root, 
            ColliderSensor colliderSensor,
            AtomicVariable<ResourceModel> targetResource, 
            AtomicVariable<bool> canGathering, 
            AtomicVariable<Vector3> moveDirection)
        {
            _root = root;
            _colliderSensor = colliderSensor;
            _targetResource = targetResource;
            _canGathering = canGathering;
            _moveDirection = moveDirection;
        }

        public void FixedUpdate()
        {
            if (_moveDirection.Value != Vector3.zero)
            {
                ClearTargetResource();
                return;
            }
            
            var resources = _colliderSensor.Targets;

            if (resources[0] == null)
            {
                ClearTargetResource();
                return;
            }          
            
            var closetResource = GetClosetResource(resources);

            if (closetResource.Amount.Value == 0)
            {
                ClearTargetResource();
                return;
            }

            _targetResource.Value = closetResource;
            _canGathering.Value = true;
        }

        private void ClearTargetResource()
        {
            _canGathering.Value = false;
            _targetResource.Value = null;
        }

        private ResourceModel GetClosetResource(Collider[] resources)
        {
            var closetResource = resources[0];
            var minDistance = Vector3.Distance(_root.position, closetResource.transform.position);

            foreach (var resource in resources)
            {
                if (resource == null)
                {
                    continue;
                }

                var distance = Vector3.Distance(_root.position, resource.transform.position);

                if (distance < minDistance)
                {
                    minDistance = distance;
                    closetResource = resource;
                }
            }
            
            return closetResource.GetComponent<ResourceModel>();
        }
    }
}