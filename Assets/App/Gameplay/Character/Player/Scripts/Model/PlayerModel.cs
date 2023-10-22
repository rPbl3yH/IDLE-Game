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
        public AtomicVariable<float> Delay;
        public AtomicVariable<bool> CanUnloadResources;

        public ResourceStorage ResourceStorage;

        [SerializeField] private ColliderSensor _barnSensor;
        [SerializeField] private ColliderSensor _resourceSensor;

        //Логика
        private MovementMechanics _movementMechanics;
        private RotateMechanics _rotateMechanics;
        private DetectionResourceMechanics _detectionResourceMechanics;
        private DetectionBarnMechanics _detectionBarnMechanics;
        private GatheringResourceMechanics _gatheringResourceMechanics;
        private UnloadResourcesObserver _unloadResourcesObserver;
        private UnloadingResourcesMechanics _unloadingResourcesMechanics;

        private void Awake()
        {
            ResourceStorage = new ResourceStorage();
            _movementMechanics = new MovementMechanics(Root, MoveDirection, Speed);
            _rotateMechanics = new RotateMechanics(View, MoveDirection);
            _detectionBarnMechanics = new DetectionBarnMechanics(LevelStorage, _barnSensor);
            _detectionResourceMechanics =
                new DetectionResourceMechanics(Root, TargetResource, CanGathering, DetectionRadius, MoveDirection);
            _gatheringResourceMechanics = new GatheringResourceMechanics(ResourceStorage, TargetResource, GatheringCount, Gathered);
            _unloadResourcesObserver = new UnloadResourcesObserver(MoveDirection, CanUnloadResources, LevelStorage);
            _unloadingResourcesMechanics = new UnloadingResourcesMechanics(LevelStorage, CanUnloadResources, Delay, ResourceStorage);
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
            var deltaTime = Time.deltaTime;
            
            _movementMechanics.Update(deltaTime);
            _rotateMechanics.Update();
            _unloadResourcesObserver.Update();
            _unloadingResourcesMechanics.Update(deltaTime);
        }
    }
}