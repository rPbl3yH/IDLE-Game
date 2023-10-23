using System;
using System.Collections.Generic;
using App.Gameplay.Resource;
using UnityEngine;

namespace App.Gameplay
{
    public class ResourceDetectController : MonoBehaviour
    {
        [SerializeField] private CharacterModel _characterModel;
        [SerializeField] private ColliderSensor _colliderSensor;
        [SerializeField] private ResourceService _resourceService;

        private void OnEnable()
        {
            _colliderSensor.ColliderUpdated += OnColliderUpdated;
        }

        private void OnDisable()
        {
            _colliderSensor.ColliderUpdated -= OnColliderUpdated;
        }

        private void Update()
        {
            _characterModel.TargetResource.Value = _resourceService.GetClosetResource(_characterModel.Root);
        }

        private void OnColliderUpdated(Collider[] colliders)
        {
            var result = new List<ResourceModel>();
            
            foreach (var collider1 in colliders)
            {
                if (collider1 == null)
                {
                    continue;
                }
                
                if (collider1.TryGetComponent(out ResourceModel resourceModel))
                {
                    result.Add(resourceModel);
                }
            }

            
        }
    }
}