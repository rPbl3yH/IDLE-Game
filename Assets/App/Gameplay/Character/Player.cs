using Atomic;
using UnityEngine;

namespace App.Gameplay.Character
{
    public class Player : MonoBehaviour
    {
        //Данные
        public AtomicVariable<Vector3> MoveDirection = new();
        public AtomicVariable<float> Speed = new();
        public Transform Root;
        public Transform View;
        
        //Логика
        private MovementMechanics _movementMechanics;
        private RotateMechanics _rotateMechanics;

        private void Awake()
        {
            _movementMechanics = new MovementMechanics(Root, MoveDirection, Speed);
            _rotateMechanics = new RotateMechanics(View, MoveDirection);
        }

        private void Update()
        {
            _movementMechanics.Update(Time.deltaTime);
            _rotateMechanics.Update();
        }
    }
}