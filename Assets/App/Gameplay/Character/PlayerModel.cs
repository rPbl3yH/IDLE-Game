using App.Gameplay.Movement;
using Declarative;
using UnityEngine;
using VContainer;

namespace App.Gameplay.Character
{
    public class PlayerModel : DeclarativeModel
    {
        [Inject] private InputController _inputController;
        
        [SerializeField] private float _speed;

        public PlayerModel()
        {
            onUpdate += deltaTime =>
            {
                transform.localPosition += _inputController.MoveDirection * _speed * deltaTime;
            };
        }
    }
}