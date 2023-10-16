using System;
using App.Gameplay.Character;
using UnityEngine;

namespace App.Gameplay.Movement
{
    public class InputController
    {
        private readonly IInputHandler _inputHandler;

        public Vector3 MoveDirection => _moveDirection;
        
        private Vector3 _moveDirection;

        public InputController(IInputHandler inputHandler)
        {
            _inputHandler = inputHandler;
            _inputHandler.DirectionChanged += OnDirectionChanged;
        }

        private void OnDirectionChanged(Vector2 inputDirection)
        {
            _moveDirection = new Vector3(inputDirection.x, 0f, inputDirection.y);
        }
    }
}