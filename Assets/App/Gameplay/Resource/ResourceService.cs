using System;
using System.Collections.Generic;
using System.Linq;
using App.Gameplay.Resource.Model;
using App.Gameplay.ResourceStorage;
using UnityEngine;

namespace App.Gameplay.Resource
{
    public class ResourceService : MonoBehaviour
    {
        [SerializeField] private List<ResourceModel> _resources;
        
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