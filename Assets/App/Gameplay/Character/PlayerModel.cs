using System;
using App.Gameplay.Movement;
using Atomic;
using Declarative;
using UnityEngine;
using VContainer;

namespace App.Gameplay.Character
{
    public class PlayerModel : DeclarativeModel
    {
        [Inject] 
        private InputController _inputController;

        [Section]
        public MoveSection MoveSection;
    }

    [Serializable]
    public class MoveSection
    {
        [SerializeField] private float _speed;

        public AtomicVariable<Vector3> MoveDirection = new();

        [Construct]
        public void Construct(DeclarativeModel model)
        {
            model.onUpdate += deltaTime =>
            {
                model.transform.localPosition += MoveDirection.Value * _speed * deltaTime;
            };
        }
    }
}