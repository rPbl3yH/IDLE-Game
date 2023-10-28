using System;
using SimpleInputNamespace;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace App.GameEngine.Input.Handlers
{
    public class InputHandler : IInputHandler, ITickable
    {
        public event Action<Vector2> DirectionChanged;

        [Inject]
        private Joystick _joystick;

        private Vector2 _currentDirection;

        public void Tick()
        {
            if (_currentDirection == _joystick.Value)
            {
                return;
            }
        
            _currentDirection = _joystick.Value;
            DirectionChanged?.Invoke(_currentDirection);
        }
    }
}