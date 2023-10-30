using System;
using SimpleInputNamespace;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace App.GameEngine.Input.Handlers
{
    public class JoystickInputHandler : IInputHandler, ITickable
    {
        public event Action<Vector3> DirectionChanged;

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
            var direction = new Vector3(_currentDirection.x, 0, _currentDirection.y);
            DirectionChanged?.Invoke(direction);
        }
    }
}