using System;
using System.Collections.Generic;
using App.Gameplay.LevelStorage;
using App.Gameplay.Resource;
using Atomic;
using UnityEngine;

namespace App.Gameplay
{
    public class PlayerModel : MonoBehaviour
    {
        //Данные
        public Transform Root;
        public Transform View;
        
        public AtomicVariable<Vector3> MoveDirection;
        public AtomicVariable<float> Speed;

        public AtomicVariable<float> DetectionRadius;
        public AtomicVariable<ResourceModel> TargetResource;
        public AtomicVariable<bool> CanGathering;
        public AtomicVariable<int> GatheringCount;
        public AtomicEvent Gathered;

        public AtomicVariable<LevelStorageModel> LevelStorage;

        public ResourceStorage ResourceStorage;

        [SerializeField] private ColliderSensor _barnSensor;
        [SerializeField] private ColliderSensor _resourceSensor;

        //Логика
        private MovementMechanics _movementMechanics;
        private RotateMechanics _rotateMechanics;
        private DetectionResourceMechanics _detectionResourceMechanics;
        private DetectionBarnMechanics _detectionBarnMechanics;
        private GatheringResourceMechanics _gatheringResourceMechanics;

        private void Awake()
        {
            ResourceStorage = new ResourceStorage();
            _movementMechanics = new MovementMechanics(Root, MoveDirection, Speed);
            _rotateMechanics = new RotateMechanics(View, MoveDirection);
            _detectionBarnMechanics = new DetectionBarnMechanics(LevelStorage, _barnSensor);
            _detectionResourceMechanics =
                new DetectionResourceMechanics(Root, TargetResource, CanGathering, DetectionRadius, MoveDirection);
            _gatheringResourceMechanics = new GatheringResourceMechanics(ResourceStorage, TargetResource, GatheringCount, Gathered);
        }

        private void OnEnable()
        {
            _gatheringResourceMechanics.OnEnable();
            _detectionBarnMechanics.OnEnable();
        }

        private void OnDisable()
        {
            _gatheringResourceMechanics.OnDisable();
            _detectionBarnMechanics.OnDisable();
        }

        private void FixedUpdate()
        {
            _detectionResourceMechanics.FixedUpdate();
        }

        private void Update()
        {
            _movementMechanics.Update(Time.deltaTime);
            _rotateMechanics.Update();
        }
    }
}