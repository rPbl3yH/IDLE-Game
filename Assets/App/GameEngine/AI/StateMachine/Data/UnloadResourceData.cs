using System;
using App.Gameplay.LevelStorage;

namespace App.GameEngine.AI
{
    [Serializable]
    public class UnloadResourceData
    {
        public bool IsEnable;
        public BarnService BarnService;
    }
}