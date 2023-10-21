using Atomic;
using UnityEngine;

namespace App.Gameplay
{
    public class PlayerAnimationController
    {
        private static readonly int MainState = Animator.StringToHash("MainState");
        private const int IDLE_STATE = 0;
        private const int RUN_STATE = 1;
        
        private readonly AtomicVariable<Vector3> _moveDirection;
        private readonly Animator _animator;

        public PlayerAnimationController(AtomicVariable<Vector3> moveDirection, Animator animator)
        {
            _moveDirection = moveDirection;
            _animator = animator;
        }

        public void Update()
        {
            int state = GetAnimatorState();
            _animator.SetInteger(MainState, state);
        }

        private int GetAnimatorState()
        {
            if (_moveDirection.Value != Vector3.zero)
            {
                return RUN_STATE;
            }

            return IDLE_STATE;
        }
    }
}