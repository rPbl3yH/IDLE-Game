using System.Collections.Generic;

namespace App.Gameplay.LevelStorage
{
    public class BuildingModel : ResourceStorageModel
    {
        private void OnEnable()
        {
            ResourceStorage.ResourcesChanged += OnResourcesChanged;   
        }

        private void OnDisable()
        {
            ResourceStorage.ResourcesChanged -= OnResourcesChanged;
        }

        private void OnResourcesChanged(Dictionary<ResourceType, ResourceValue> value)
        {
            if (ResourceStorage.IsFull())
            {
                
                Build();
            }
            else
            {
                print("Not full");
            }
        }

        private void Build()
        {
            print("Build");
        }
    }
}