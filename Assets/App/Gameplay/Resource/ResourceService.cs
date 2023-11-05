﻿using System.Collections.Generic;
using System.Linq;
using App.Gameplay.Resource.Model;
using UnityEngine;

namespace App.Gameplay.Resource
{
    public class ResourceService : MonoBehaviour
    {
        [SerializeField] private List<ResourceModel> _resources;

        public List<ResourceModel> AllResources
        {
            get => _resources;
            set => _resources = value;
        }

        public void SetActiveResourceType(ResourceType resourceType, bool value)
        {
            foreach (var resource in _resources)
            {
                if (resource.ResourceType == resourceType)
                {
                    resource.IsEnable.Value = value;
                }
            }
        }
        
        public ResourceModel GetClosetResource(Transform point, ResourceType resourceType)
        {
            var list = _resources.OrderBy(model => Vector3.Distance(model.transform.position, point.position));
            var resource = list.FirstOrDefault(model => model.ResourceType == resourceType && model.Amount.Value > 0);
            
            return resource;
        }
        
        public ResourceModel GetClosetResource(Transform point)
        {
            var list = _resources.OrderBy(model => Vector3.Distance(model.transform.position, point.position));
            var resource = list.FirstOrDefault(model => model.Amount.Value > 0);
            
            return resource;
        }
    }
}