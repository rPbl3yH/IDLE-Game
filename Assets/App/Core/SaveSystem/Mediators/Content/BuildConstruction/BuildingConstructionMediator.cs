using System.Linq;
using App.Gameplay.Building;

namespace App.Core.SaveSystem.Mediators.Content
{
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