using System;
using UnityEngine;

namespace App.Gameplay.LevelStorage
{
    [Serializable]
    public class ResourceData
    {
        public ResourceType Type => _resourceType;
        public int Count => _count;

        [SerializeField] private ResourceType _resourceType;
        [SerializeField] private int _count;

        public ResourceData(ResourceType type, int count)
        {
            _resourceType = type;
            _count = count;
        }
    }
}