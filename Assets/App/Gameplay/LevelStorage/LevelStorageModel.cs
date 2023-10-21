using System;
using Atomic;
using UnityEngine;

namespace App.Gameplay.LevelStorage
{
    [Serializable]
    public struct ResourceData
    {
        public readonly int Count;
        public readonly ResourceType Type;

        public ResourceData(int count, ResourceType type)
        {
            Count = count;
            Type = type;
        }
    }
    
    public class LevelStorageModel : MonoBehaviour
    {
        private ResourceStorage _resourceStorage;

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
            _resourceStorage.Add(resourceData.Type, resourceData.Count);
        }
    }
}