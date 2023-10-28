using System;
using App.Gameplay.ResourceStorage;

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
}