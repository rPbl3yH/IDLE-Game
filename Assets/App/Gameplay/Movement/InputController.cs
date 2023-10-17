using System;
using App.Gameplay.Character;
using UnityEngine;
using VContainer;

namespace App.Gameplay.Movement
{
    public class InputController
    {
        private readonly IInputHandler _inputHandler;

        private Vector3 _moveDirection;

        private PlayerModel _playerModel;
        
        [Inject]
        public InputController(IInputHandler inputHandler, PlayerModel playerModel)
        {
            _inputHandler = inputHandler;
            _playerModel = playerModel;
            _inputHandler.DirectionChanged += OnDirectionChanged;
            Debug.Log("Print input controller");
        }

        private void OnDirectionChanged(Vector2 inputDirection)
        {
            _moveDirection = new Vector3(inputDirection.x, 0f, inputDirection.y);
            _playerModel.MoveSection.MoveDirection.Value = _moveDirection;
        }
    }
}