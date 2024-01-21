using Atomic.Elements;
using UnityEngine;

namespace App.Gameplay.Character.Scripts.Model.Mechanics
{
    public class RotateMechanics
    {
        private readonly IAtomicVariable<Vector3> _moveDirection;
        private readonly Transform _view;

        public RotateMechanics(Transform view, IAtomicVariable<Vector3> moveDirection)
        {
            _view = view;
            _moveDirection = moveDirection;
        }

        public void Update()
        {
            if (_moveDirection.Value.sqrMagnitude == 0)
            {
                return;
            }

            _view.rotation = Quaternion.LookRotation(_moveDirection.Value);
        }
    }
}