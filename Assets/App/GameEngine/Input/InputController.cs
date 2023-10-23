﻿using System;
using UnityEngine;
using VContainer.Unity;

namespace App.Gameplay.Movement
{
    public class InputController : IStartable, IDisposable
    {
        private readonly IInputHandler _inputHandler;

        private readonly CharacterModel _characterModel;
        
        private Vector3 _moveDirection;
        
        public InputController(IInputHandler inputHandler, CharacterModel characterModel)
        {
            _inputHandler = inputHandler;
            _characterModel = characterModel;
        }
        
        public void Start()
        {
            _inputHandler.DirectionChanged += OnDirectionChanged;
        }
        
        private void OnDirectionChanged(Vector2 inputDirection)
        {
            _moveDirection = new Vector3(inputDirection.x, 0f, inputDirection.y);
            _characterModel.MoveDirection.Value = _moveDirection;
        }

        public void Dispose()
        {
            _inputHandler.DirectionChanged -= OnDirectionChanged;
        }
    }
}