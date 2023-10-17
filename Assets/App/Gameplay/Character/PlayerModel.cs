using System;
using Atomic;
using Declarative;
using UnityEngine;

namespace App.Gameplay.Character
{
    public class PlayerModel : DeclarativeModel
    {
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