using System;
using System.Collections.Generic;
using System.Linq;
using App.Gameplay.LevelStorage;
using App.Gameplay.Player;
using Sirenix.OdinInspector;
using UnityEngine;

namespace App.Gameplay
{
    [Serializable]
    public class ResourceStorage
    {
        public event Action<Dictionary<ResourceType, ResourceValue>> ResourcesChanged;

        public Dictionary<ResourceType, ResourceValue> Resources => _storage;
        public ResourceStorageConfig StorageConfig
        {
            get => _resourceStorageConfig;
            set => _resourceStorageConfig = value;
        }

        [ShowInInspector]
        private readonly Dictionary<ResourceType, ResourceValue> _storage = new();

        [SerializeField] 
        private ResourceStorageConfig _resourceStorageConfig;
        
        [Button]
        public bool TryAdd(ResourceType resourceType, int count)
        {
            if (_storage.ContainsKey(resourceType))
            {
                if (_storage[resourceType].MaxAmount == -1)
                {
                    AddResource(resourceType, count);
                    return true;
                }
                
                var availableCount = _storage[resourceType].MaxAmount - _storage[resourceType].Amount;
                
                if ( availableCount >= count)
                {
                    AddResource(resourceType, count);
                    return true;
                }

                return false;
            }

            var maxAmount = -1;
            ResourceData resource = _resourceStorageConfig.Resources.FirstOrDefault(data => data.Type == resourceType);
            
            if (resource != null)
            {
                maxAmount = resource.Count;
            }
            
            if (maxAmount == -1 || maxAmount >= count)
            {
                CreateResource(resourceType, count, maxAmount);
                return true;
            }

            return false;
        }

        public bool IsFull()
        {
            return _storage.Any(resource => resource.Value.Amount == resource.Value.MaxAmount);
        }

        public void Clear()
        {
            _storage.Clear();
        }

        public bool TryGetResource(ResourceType resourceType, out int resource)
        {
            if (_storage.TryGetValue(resourceType, out var value))
            {
                resource = value.Amount;
                return true;
            }

            resource = 0;
            return false;
        }

        [Button]
        public bool TryRemove(ResourceType resourceType, int count)
        {
            if (!CanRemove(resourceType, count))
            {
                Debug.LogWarning("Can't remove");
                return false;
            }

            _storage[resourceType].Amount -= count;
            
            if (_storage[resourceType].Amount == 0)
            {
                _storage.Remove(resourceType);
            }
            
            ResourcesChanged?.Invoke(_storage);
            return true;
        }

        private void CreateResource(ResourceType resourceType, int count, int maxAmount)
        {
            var resourceValue = new ResourceValue(count, maxAmount);
            _storage.Add(resourceType, resourceValue);
            ResourcesChanged?.Invoke(_storage);
        }

        private void AddResource(ResourceType resourceType, int count)
        {
            _storage[resourceType].Amount += count;
            ResourcesChanged?.Invoke(_storage);
        }

        private bool CanRemove(ResourceType resourceType, int count)
        {
            if (!_storage.ContainsKey(resourceType))
            {
                return false;
            }

            return _storage[resourceType].Amount >= count;
        }
    }
}