﻿using System;
using App.Gameplay.ResourceStorage;
using UnityEngine;

namespace App.Gameplay.LevelStorage
{
    [Serializable]
    public struct ResourceData
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