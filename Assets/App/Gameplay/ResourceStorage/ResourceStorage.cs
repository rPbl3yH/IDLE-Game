using System;
using System.Collections.Generic;
using App.Gameplay.LevelStorage;
using Sirenix.OdinInspector;
using UnityEngine;

namespace App.Gameplay
{
    [Serializable]
    public class ResourceStorage
    {
        public event Action<Dictionary<ResourceType, int>> ResourceChanged;

        [ShowInInspector]
        private readonly Dictionary<ResourceType, int> _storage = new();
        
        public void Add(ResourceType resourceType, int count)
        {
            if (_storage.ContainsKey(resourceType))
            {
                _storage[resourceType] += count;
            }
            else
            {
                _storage.Add(resourceType, count);
            }
            
            ResourceChanged?.Invoke(_storage);
        }

        public Dictionary<ResourceType, int> GetAllResources()
        {
            return _storage;
        }
        
        public bool TryRemove(ResourceType resourceType, int count)
        {
            if (!CanRemove(resourceType, count))
            {
                Debug.LogWarning("Can't remove");
                return false;
            }

            _storage[resourceType] -= count;
            
            if (_storage[resourceType] == 0)
            {
                _storage.Remove(resourceType);
            }
            
            ResourceChanged?.Invoke(_storage);
            return true;
        }

        private bool CanRemove(ResourceType resourceType, int count)
        {
            if (!_storage.ContainsKey(resourceType))
            {
                return false;
            }

            return _storage[resourceType] >= count;
        }
    }
}