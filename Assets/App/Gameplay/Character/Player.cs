using Atomic;
using UnityEngine;

namespace App.Gameplay.Character
{
    public class Player : MonoBehaviour
    {
        public AtomicVariable<Vector3> MoveDirection = new();
        public AtomicVariable<float> Speed = new();
        public Transform Root;
        public Transform View;
        
        //Логика
        private MovementMechanics _movementMechanics;

        private void Awake()
        {
            _movementMechanics = new MovementMechanics(MoveDirection, Speed, Root, View);
        }

        private void Update()
        {
            _movementMechanics.Update(Time.deltaTime);
        }
    }

    public class MovementMechanics
    {
        private readonly AtomicVariable<Vector3> _moveDirection;
        private readonly AtomicVariable<float> _speed;
        private readonly Transform _model;
        private readonly Transform _view;

        public MovementMechanics(
            AtomicVariable<Vector3> moveDirection, 
            AtomicVariable<float> speed, 
            Transform model, 
            Transform view)
        {
            _moveDirection = moveDirection;
            _speed = speed;
            _model = model;
            _view = view;
        }

        public void Update(float deltaTime)
        {
            if (_moveDirection.Value.sqrMagnitude == 0)
            {
                return;
            }
            
            _model.transform.position += _moveDirection.Value * deltaTime * _speed.Value;
            _view.rotation = Quaternion.LookRotation(_moveDirection.Value);
        }
    }
}