using Modules.Atomic.Values;
using UnityEngine;

namespace App.Gameplay.Character.Scripts.Visual
{
    public class CharacterAnimationController
    {
        private static readonly int MainState = Animator.StringToHash("MainState");
        private const int IDLE_STATE = 0;
        private const int RUN_STATE = 1;
        private const int GATHER_STATE = 2;

        private readonly Animator _animator;
        private readonly AtomicVariable<Vector3> _moveDirection;
        private readonly AtomicVariable<bool> _canGathering;

        public CharacterAnimationController(
            Animator animator,
            AtomicVariable<Vector3> moveDirection,
            AtomicVariable<bool> canGathering)
        {
            _moveDirection = moveDirection;
            _animator = animator;
            _canGathering = canGathering;
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

            if (_canGathering.Value)
            {
                return GATHER_STATE;
            }

            return IDLE_STATE;
        }
    }
}