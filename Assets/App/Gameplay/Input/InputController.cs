using System;
using UnityEngine;
using VContainer.Unity;

namespace App.Gameplay.Movement
{
    public class InputController : IStartable, IDisposable
    {
        private readonly IInputHandler _inputHandler;

        private readonly PlayerModel _playerModel;
        
        private Vector3 _moveDirection;
        
        public InputController(IInputHandler inputHandler, PlayerModel playerModel)
        {
            _inputHandler = inputHandler;
            _playerModel = playerModel;
        }
        
        public void Start()
        {
            _inputHandler.DirectionChanged += OnDirectionChanged;
        }
        
        private void OnDirectionChanged(Vector2 inputDirection)
        {
            _moveDirection = new Vector3(inputDirection.x, 0f, inputDirection.y);
            _playerModel.MoveDirection.Value = _moveDirection;
        }

        public void Dispose()
        {
            _inputHandler.DirectionChanged -= OnDirectionChanged;
        }
    }
}