using Atomic.Elements;
using UnityEngine;

namespace App.Gameplay.Character.Scripts.Model.Mechanics
{
    public class MovementMechanics
    {
        private readonly Transform _model;
        private readonly IAtomicValue<Vector3> _moveDirection;
        private readonly IAtomicValue<float> _speed;

        public MovementMechanics(
            Transform model,
            IAtomicValue<Vector3> moveDirection, 
            IAtomicValue<float> speed)
        {
            _model = model;
            _moveDirection = moveDirection;
            _speed = speed;
        }

        public void Update(float deltaTime)
        {
            _model.transform.position += _moveDirection.Value * (deltaTime * _speed.Value);
        }
    }
}