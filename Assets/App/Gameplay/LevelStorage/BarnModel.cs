using Modules.Atomic.Actions;
using UnityEngine;

namespace App.Gameplay.LevelStorage
{
    public class BarnModel : MonoBehaviour
    {
        public ResourceStorage ResourceStorage;

        public AtomicEvent<ResourceData> ResourceAdded;
        public Transform UnloadingPoint;

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