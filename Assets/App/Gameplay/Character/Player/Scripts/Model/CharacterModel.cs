using System;
using System.Collections.Generic;
using App.Gameplay.LevelStorage;
using App.Gameplay.Resource;
using Atomic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace App.Gameplay
{
    public class CharacterModel : MonoBehaviour
    {
        //Данные
        public Transform Root;
        public Transform View;
        
        [Header("Movement")]
        public AtomicVariable<Vector3> MoveDirection;
        public AtomicVariable<float> Speed;

        [Header("Gathering")]
        public AtomicVariable<ResourceModel> TargetResource;
        public AtomicVariable<float> GatheringDistance;
        public AtomicVariable<bool> CanGathering;
        public AtomicVariable<int> GatheringCount;
        public AtomicEvent Gathered;

        [Header("Resources")] 
        public AtomicVariable<ResourceType> ResourceType;
        public AtomicVariable<int> Amount;
        public AtomicVariable<int> MaxAmount;
        [ShowInInspector, ReadOnly]
        public AtomicVariable<bool> IsFreeSpace;

        [Header("Unload Resources")]
        public AtomicVariable<LevelStorageModel> LevelStorage;
        public AtomicVariable<float> Delay;
        public AtomicVariable<bool> CanUnloadResources;

        [SerializeField] private ColliderSensor _barnSensor;

        //Логика
        private MovementMechanics _movementMechanics;
        private RotateMechanics _rotateMechanics;
        private DetectionResourceMechanics _detectionResourceMechanics;
        private DetectionBarnMechanics _detectionBarnMechanics;
        private GatheringResourceMechanics _gatheringResourceMechanics;
        private UnloadResourcesObserver _unloadResourcesObserver;
        private UnloadingResourcesMechanics _unloadingResourcesMechanics;
        private FreeSpaceResourceMechanic _freeSpaceResourceMechanic;

        private void Awake()
        {
            _movementMechanics = new MovementMechanics(Root, MoveDirection, Speed);
            _rotateMechanics = new RotateMechanics(View, MoveDirection);
            _detectionBarnMechanics = new DetectionBarnMechanics(LevelStorage, _barnSensor);
            _detectionResourceMechanics =
                new DetectionResourceMechanics(Root, TargetResource, GatheringDistance, CanGathering, IsFreeSpace, MoveDirection);
            _gatheringResourceMechanics = new GatheringResourceMechanics(TargetResource, GatheringCount, Amount, MaxAmount, Gathered);
            _unloadResourcesObserver = new UnloadResourcesObserver(MoveDirection, CanUnloadResources, LevelStorage);
            _unloadingResourcesMechanics = new UnloadingResourcesMechanics(LevelStorage, CanUnloadResources, Delay, ResourceType, Amount);
            _freeSpaceResourceMechanic = new FreeSpaceResourceMechanic(IsFreeSpace, Amount, MaxAmount);
        }

        private void OnEnable()
        {
            _gatheringResourceMechanics.OnEnable();
            _detectionBarnMechanics.OnEnable();
            _freeSpaceResourceMechanic.OnEnable();
        }

        private void OnDisable()
        {
            _gatheringResourceMechanics.OnDisable();
            _detectionBarnMechanics.OnDisable();
            _freeSpaceResourceMechanic.OnDisable();
        }

        private void Update()
        {
            var deltaTime = Time.deltaTime;
            _movementMechanics.Update(deltaTime);
            _rotateMechanics.Update();
            _detectionResourceMechanics.Update();
            _unloadResourcesObserver.Update();
            _unloadingResourcesMechanics.Update(deltaTime);
        }
    }
}