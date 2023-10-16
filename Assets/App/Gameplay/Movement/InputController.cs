using System;
using UnityEngine;

namespace App.Gameplay.Movement
{
    public class InputController : MonoBehaviour
    {
        [SerializeField] private InputHandler _inputHandler;
        [SerializeField] private GameObject _player;

        private Vector3 _moveDirection;
        
        private void OnEnable()
        {
            _inputHandler.DirectionChanged += OnDirectionChanged;
        }

        private void OnDirectionChanged(Vector2 inputDirection)
        {
            _moveDirection = new Vector3(inputDirection.x, 0f, inputDirection.y);
        }

        private void Update()
        {
            _player.transform.position += _moveDirection * Time.deltaTime;
        }
    }
}