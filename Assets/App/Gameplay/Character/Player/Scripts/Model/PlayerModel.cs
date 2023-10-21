using System;
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

        public AtomicEvent Gathered;

        public Transform Root;
        public Transform View;
        
        //Логика
        private MovementMechanics _movementMechanics;
        private RotateMechanics _rotateMechanics;
        private DetectionResourceMechanics _detectionResourceMechanics;
        private GatheringResourceMechanics _gatheringResourceMechanics;
        
        private void Awake()
        {
            _movementMechanics = new MovementMechanics(Root, MoveDirection, Speed);
            _rotateMechanics = new RotateMechanics(View, MoveDirection);
            _detectionResourceMechanics =
                new DetectionResourceMechanics(Root, TargetResource, CanGathering, DetectionRadius, MoveDirection);
            _gatheringResourceMechanics = new GatheringResourceMechanics(Gathered);
        }

        private void OnEnable()
        {
            _gatheringResourceMechanics.OnEnable();
        }

        private void OnDisable()
        {
            _gatheringResourceMechanics.OnDisable();
        }

        private void Update()
        {
            _movementMechanics.Update(Time.deltaTime);
            _rotateMechanics.Update();
            _detectionResourceMechanics.Update();
        }
    }

    public class GatheringResourceMechanics
    {
        private readonly AtomicEvent _gathered;

        public GatheringResourceMechanics(AtomicEvent gathered)
        {
            _gathered = gathered;
        }

        public void OnEnable()
        {
            _gathered.AddListener(OnGathered);
        }

        public void OnDisable()
        {
            _gathered.RemoveListener(OnGathered);
        }

        private void OnGathered()
        {
            Debug.Log("Gathered in player");
        }
    }
}