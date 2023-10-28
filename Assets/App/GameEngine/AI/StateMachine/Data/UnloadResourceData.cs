using System;
using App.Gameplay.LevelStorage;

namespace App.GameEngine.AI.StateMachine.Data
{
    [Serializable]
    public class UnloadResourceData
    {
        public bool IsEnable;
        public BarnService BarnService;
    }
}