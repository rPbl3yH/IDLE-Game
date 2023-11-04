﻿using System.Collections.Generic;
using System.Linq;
using App.Gameplay;
using App.Gameplay.Building;
using App.Gameplay.LevelStorage;

namespace App.Core.SaveSystem.Mediators.Content
{
    public class BuildingConstructionData
    {
        public List<Dictionary<ResourceType, ResourceValue>> Resources;

        public BuildingConstructionData()
        {
            Resources = new List<Dictionary<ResourceType, ResourceValue>>();
        }
    }
    
    public class BuildingConstructionMediator : GameMediator<BuildingConstructionData, BuildingConstructionService>
    {
        protected override void SetupFromData(BuildingConstructionService service, BuildingConstructionData data)
        {
            var services = service.GetServices().ToList();
            
            for (int i = 0; i < services.Count; i++)
            {
                var resourceStorage = services[i].ResourceStorage;
                resourceStorage.Clear();
                
                var resources = data.Resources[i];
                
                foreach (var resource in resources)
                {
                    resourceStorage.TryAdd(resource.Key, resource.Value.Amount);
                }
            }
        }

        protected override void SetupByDefault(BuildingConstructionService service)
        {
            //throw new System.NotImplementedException();
        }

        protected override BuildingConstructionData ConvertToData(BuildingConstructionService service)
        {
            var data = new BuildingConstructionData();
            var constructionModels = service.GetServices();
            
            foreach (var model in constructionModels)
            {
                data.Resources.Add(model.ResourceStorage.Resources);
            }

            return data;
        }
    }
}