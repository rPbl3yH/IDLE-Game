using System;
using UnityEngine;

namespace App.GameEngine.Input.Handlers
{
    public interface IInputHandler
    {
        event Action<Vector2> DirectionChanged;
        
        bool IsEnabled { get; }

        public void Enable();
        public void Disable();
    }
}