using System;
using SimpleInputNamespace;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace App.GameEngine.Input.Handlers
{
    public class JoystickInputHandler : IInputHandler, ITickable
    {
        public event Action<Vector2> DirectionChanged;

        public bool IsEnabled { private set; get; }

        [Inject]
        private Joystick _joystick;

        private Vector2 _currentDirection;

        public void Tick()
        {
            if (!IsEnabled)
            {
                return;
            }
            
            if (_currentDirection == _joystick.Value)
            {
                return;
            }
        
            _currentDirection = _joystick.Value;
            DirectionChanged?.Invoke(_currentDirection);
        }

        public void Enable()
        {
            IsEnabled = true;
            _joystick.OnEnable();
        }

        public void Disable()
        {
            IsEnabled = false;
            _joystick.OnDisable();
        }
    }
}