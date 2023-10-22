using System;
using Atomic;
using UnityEngine;

namespace App.Gameplay.LevelStorage
{
    [Serializable]
    public struct ResourceData
    {
        public readonly ResourceType Type;
        public readonly int Count;

        public ResourceData(ResourceType type, int count)
        {
            Type = type;
            Count = count;
        }
    }
    
    public class LevelStorageModel : MonoBehaviour
    {
        public ResourceStorage ResourceStorage;

        public AtomicEvent<ResourceData> ResourceAdded;

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
            ResourceStorage.Add(resourceData.Type, resourceData.Count);
        }
    }
}