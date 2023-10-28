using Modules.Atomic.Values;
using UnityEngine;

namespace App.Gameplay.Character.Scripts.Model.Mechanics
{
    public class MovementMechanics
    {
        private readonly Transform _model;

        private readonly AtomicVariable<Vector3> _moveDirection;

        private readonly AtomicVariable<float> _speed;
        private readonly IAtomicValue<bool> _canMove;

        public MovementMechanics(
            Transform model,
            AtomicVariable<Vector3> moveDirection, 
            AtomicVariable<float> speed)
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