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
        [SerializeField] private Transform _view;
        
        public AtomicVariable<Vector3> MoveDirection = new();

        [Construct]
        public void Construct(DeclarativeModel model)
        {
            model.onUpdate += deltaTime =>
            {
                if (MoveDirection.Value.sqrMagnitude == 0)
                {
                    return;
                }

                model.transform.position += MoveDirection.Value * deltaTime * _speed;
                _view.rotation = Quaternion.LookRotation(MoveDirection.Value);
            };
        }
    }
}