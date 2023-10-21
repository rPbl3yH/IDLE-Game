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
        private readonly AtomicVariable<float> _detectionRadius;
        private readonly AtomicVariable<Vector3> _moveDirection;
        private readonly Transform _root;

        private readonly int _layerMask = LayerMask.GetMask("Resource");

        public DetectionResourceMechanics(
            Transform root, 
            AtomicVariable<ResourceModel> targetResource, 
            AtomicVariable<bool> canGathering, 
            AtomicVariable<float> detectionRadius,
            AtomicVariable<Vector3> moveDirection)
        {
            _root = root;
            _targetResource = targetResource;
            _canGathering = canGathering;
            _detectionRadius = detectionRadius;
            _moveDirection = moveDirection;
        }

        public void FixedUpdate()
        {
            if (_moveDirection.Value != Vector3.zero)
            {
                ClearTargetResource();
                return;
            }
            
            var resources = DetectionUtils.GetDetectionResources(_root.position, _detectionRadius.Value, _layerMask);

            if (resources.Count == 0)
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

        private ResourceModel GetClosetResource(List<Collider> resources)
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

    public static class DetectionUtils
    {
        public static List<Collider> GetDetectionResources(Vector3 center, float detectionRadius, int layerMask)
        {
            var result = new List<Collider>();
            var resources = new Collider[3];
            var size = Physics.OverlapSphereNonAlloc(center, detectionRadius, resources, layerMask);

            if (size == 0)
            {
                return result;
            }

            for (int i = 0; i < size; i++)
            {
                result.Add(resources[i]);                
            }
            
            return result;
        }
    }
}