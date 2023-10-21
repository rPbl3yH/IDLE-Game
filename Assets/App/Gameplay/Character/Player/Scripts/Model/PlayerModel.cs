using App.Gameplay.Resource;
using Atomic;
using UnityEngine;

namespace App.Gameplay
{
    public class PlayerModel : MonoBehaviour
    {
        //Данные
        public AtomicVariable<Vector3> MoveDirection;
        public AtomicVariable<float> Speed;
        
        public AtomicVariable<float> DetectionRadius;
        public AtomicVariable<ResourceModel> TargetResource;
        public AtomicVariable<bool> CanGathering;

        public Transform Root;
        public Transform View;
        
        //Логика
        private MovementMechanics _movementMechanics;
        private RotateMechanics _rotateMechanics;
        private DetectionResourceMechanics _detectionResourceMechanics;

        private void Awake()
        {
            _movementMechanics = new MovementMechanics(Root, MoveDirection, Speed);
            _rotateMechanics = new RotateMechanics(View, MoveDirection);
            _detectionResourceMechanics =
                new DetectionResourceMechanics(Root, TargetResource, CanGathering, DetectionRadius, MoveDirection);
        }

        private void Update()
        {
            _movementMechanics.Update(Time.deltaTime);
            _rotateMechanics.Update();
            _detectionResourceMechanics.Update();
        }
    }
}