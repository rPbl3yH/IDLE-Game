using System;
using System.Collections.Generic;
using System.Linq;
using App.Gameplay.LevelStorage;
using App.Gameplay.Resource;
using App.Gameplay.Resource.Model;

namespace App.Core.SaveSystem.Mediators.Content
{
    public class ResourceMediator : GameMediator<List<ResourceData>,ResourceService>
    {
        protected override void SetupFromData(ResourceService service, List<ResourceData> data)
        {
            for (int i = 0; i < data.Count; i++)
            {
                service.AllResources[i].Amount.Value = data[i].Count;
                // service.AllResources[i].ResourceType = data[i].Type;
            }
        }

        protected override void SetupByDefault(ResourceService service)
        {
            
        }

        protected override List<ResourceData> ConvertToData(ResourceService service)
        {
            var result = new List<ResourceData>();
            
            foreach (var resource in service.AllResources)
            {
                var data = new ResourceData(resource.ResourceType, resource.Amount.Value);
                result.Add(data);
            }
            
            return result;
        }
    }
}