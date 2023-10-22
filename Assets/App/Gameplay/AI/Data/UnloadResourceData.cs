using System;
using App.Gameplay.LevelStorage;

namespace App.Gameplay.AI
{
    [Serializable]
    public class UnloadResourceData
    {
        public bool IsEnable;
        public LevelStorageService LevelStorageService;
    }
}