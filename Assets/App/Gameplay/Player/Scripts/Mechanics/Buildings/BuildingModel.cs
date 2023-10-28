using System.Linq;
using App.Gameplay.LevelStorage;
using App.Gameplay.ResourceStorage;
using Modules.Atomic.Actions;
using Sirenix.OdinInspector;
using UnityEngine;

namespace App.Gameplay.Player
{
    public class BuildingModel : MonoBehaviour
    {
        public ResourceStorage.ResourceStorage ResourceStorage;
        public ResourceStorageConfig ResourceStorageConfig;
        public AtomicEvent<ResourceData> ResourceAdded;
        public Transform UnloadingPoint;

        [Button]
        public bool CanAdd(ResourceType resourceType)
        {
            if (ResourceStorage.TryGetResource(resourceType, out var resourceValue))
            {
                var resourceData =
                    ResourceStorageConfig.Resources.FirstOrDefault(data => data.Key == resourceType);
                var result = resourceData.Value > resourceValue; 
                print(result);
                return result;
            }

            print(false);
            return false;
        }
        
        private void OnEnable()
        {
            ResourceAdded.AddListener(OnResourceAdded);
        }

        private void OnDisable()
        {
            ResourceAdded.RemoveListener(OnResourceAdded);
        }

        private void OnResourceAdded(ResourceData resourceData)
        {
            print($"Resources Added {resourceData.Type} {resourceData.Count}");
            ResourceStorage.TryAdd(resourceData.Type, resourceData.Count);
        }
    }
}