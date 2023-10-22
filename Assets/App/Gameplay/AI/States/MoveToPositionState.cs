using App.Gameplay.AI.Model;
using Atomic;
using UnityEngine;

namespace App.Gameplay.AI.States
{
    public class MoveToPositionState : IState
    {
        private readonly MoveToPositionData _moveToPositionData;
        private readonly AtomicVariable<Vector3> _moveDirection;
        private readonly Transform _root;

        public MoveToPositionState(MoveToPositionData moveToPositionData, AtomicVariable<Vector3> moveDirection, Transform root)
        {
            _moveToPositionData = moveToPositionData;
            _moveDirection = moveDirection;
            _root = root;
        }

        public void Enter()
        {
        }

        public void Update(float deltaTime)
        {
            if (!_moveToPositionData.IsEnable)
            {
                return;
            }

            var delta = _moveToPositionData.TargetPosition - _root.position;
            var distance = delta.magnitude;

            _moveToPositionData.IsPositionReached = distance <= _moveToPositionData.StoppingDistance;

            if (_moveToPositionData.IsPositionReached)
            {
                _moveDirection.Value = Vector3.zero;
            }
            else
            {
                _moveDirection.Value = delta.normalized;
            }
        }

        public void Exit()
        {
            _moveDirection.Value = Vector3.zero;
        }
    }
}