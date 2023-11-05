using System.Linq;

namespace App.Core.SaveSystem.Mediators.Content
{
    public class BarnModelMediator : GameMediator<BarnModelData, BarnModelService>
    {
        protected override void SetupFromData(BarnModelService service, BarnModelData data)
        {
            var services = service.GetServices().ToList();
            
            for (int i = 0; i < data.Resources.Count; i++)
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

        protected override void SetupByDefault(BarnModelService service)
        {
            // throw new System.NotImplementedException();
        }

        protected override BarnModelData ConvertToData(BarnModelService service)
        {
            var data = new BarnModelData();
            var constructionModels = service.GetServices();
            
            foreach (var model in constructionModels)
            {
                data.Resources.Add(model.ResourceStorage.Resources);
            }

            return data;
        }
    }
}