using System;
using Atomic;
using UnityEngine;

namespace App.Gameplay
{
    public class PlayerAnimationController
    {
        private static readonly int MainState = Animator.StringToHash("MainState");
        private const int Idle = 0;
        private const int Run = 1;
        
        private readonly AtomicVariable<Vector3> _moveDirection;
        private readonly Animator _animator;

        public PlayerAnimationController(AtomicVariable<Vector3> moveDirection, Animator animator)
        {
            _moveDirection = moveDirection;
            _animator = animator;
        }

        public void Update()
        {
            if (_moveDirection.Value.sqrMagnitude > 0)
            {
                _animator.SetInteger(MainState, Run);
            }
            else
            {
                _animator.SetInteger(MainState, Idle);
            }
        }
    }
}