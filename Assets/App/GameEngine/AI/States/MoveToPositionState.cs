﻿using App.Gameplay.AI.Model;
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
            _moveToPositionData.IsEnable = true;
            CheckDistance(GetDirection());
        }
        
        public void Update(float deltaTime)
        {
            if (!_moveToPositionData.IsEnable)
            {
                return;
            }

            var delta = GetDirection();
            CheckDistance(delta);

            if (_moveToPositionData.IsPositionReached)
            {
                _moveDirection.Value = Vector3.zero;
            }
            else
            {
                _moveDirection.Value = delta.normalized;
            }
        }

        private Vector3 GetDirection()
        {
            return _moveToPositionData.TargetPosition - _root.position;
        }

        private void CheckDistance(Vector3 delta)
        {
            _moveToPositionData.IsPositionReached = delta.magnitude <= _moveToPositionData.StoppingDistance;
        }

        public void Exit()
        {
            _moveDirection.Value = Vector3.zero;
            _moveToPositionData.IsEnable = false;
        }
    }
}