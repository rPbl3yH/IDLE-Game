using System.Collections.Generic;
using System.Linq;
using App.Gameplay.LevelStorage;
using App.Gameplay.Resource.Model;
using UnityEngine;

namespace App.Gameplay.Resource
{
    public class ResourceSpawner 
    {
        
    }
    
    public class ResourceGameService : GameService<ResourceModel>
    {
        public void SetActiveResourceType(ResourceType resourceType, bool value)
        {
            foreach (var resource in Services)
            {
                if (resource.ResourceType == resourceType)
                {
                    resource.IsEnable.Value = value;
                }
            }
        }
        
        public ResourceModel GetClosetResource(Transform point, ResourceType resourceType)
        {
            var list = Services.OrderBy(model => Vector3.Distance(model.transform.position, point.position));
            var resource = list.FirstOrDefault(model => CheckCondition(model, resourceType));
            
            return resource;
        }
        
        public ResourceModel GetClosetResource(Transform point)
        {
            var list = Services.OrderBy(model => Vector3.Distance(model.transform.position, point.position));
            var resource = GetClosetResource(point, list.ElementAtOrDefault(0)!.ResourceType);
            return resource;
        }

        private bool CheckCondition(ResourceModel model, ResourceType resourceType)
        {
            return model.ResourceType == resourceType && model.Amount.Value > 0 && model.IsEnable.Value;
        }
    }

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
                    if (value)
                    {
                        resource.IsEnable.Value = true;
                    }
                    else
                    {
                        resource.IsEnable.Value = false;
                        resource.Deactivated?.Invoke();
                    }
                }
            }
        }
        
        public ResourceModel GetClosetResource(Transform point, ResourceType resourceType)
        {
            var list = _resources.OrderBy(model => Vector3.Distance(model.transform.position, point.position));
            var resource = list.FirstOrDefault(model => CheckCondition(model, resourceType));
            
            return resource;
        }
        
        public ResourceModel GetClosetResource(Transform point)
        {
            IOrderedEnumerable<ResourceModel> list = _resources.OrderBy(model => Vector3.Distance(model.transform.position, point.position));
            var resource = GetClosetResource(point, list.ElementAtOrDefault(0)!.ResourceType);
            return resource;
        }

        private bool CheckCondition(ResourceModel model, ResourceType resourceType)
        {
            return model.ResourceType == resourceType && model.Amount.Value > 0 && model.IsEnable.Value;
        }
    }
}