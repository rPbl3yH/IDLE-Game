using App.Gameplay.LevelStorage;
using Modules.Atomic.Actions;
using Sirenix.OdinInspector;
using UnityEngine;

namespace App.Gameplay.Player
{
    public class BuildingModel : MonoBehaviour
    {
        public ResourceStorage ResourceStorage;
        public AtomicEvent<ResourceData> ResourceAdded;
        public Transform UnloadingPoint;

        [Button]
        public bool CanAdd(ResourceType resourceType)
        {
            

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