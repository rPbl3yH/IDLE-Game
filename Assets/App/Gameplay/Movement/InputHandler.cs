using System;
using SimpleInputNamespace;
using UnityEngine;

namespace App.Gameplay.Movement
{
    public class InputHandler : MonoBehaviour, IInputHandler
    {
        public event Action<Vector2> DirectionChanged;
    
        [SerializeField] private Joystick _joystick;

        private Vector2 _currentDirection;
    
        private void Update()
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