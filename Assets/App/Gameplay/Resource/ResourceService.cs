using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace App.Gameplay.Resource
{
    public class ResourceService : MonoBehaviour
    {
        [SerializeField] private List<ResourceModel> _resources;
        
        public ResourceModel GetClosetResources(Transform point)
        {
            return ResourceDetectionUtils.GetClosetAvailableResource(_resources.ToArray(), point);
        }
    }
}