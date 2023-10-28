using App.Gameplay.Character.Scripts.Model.Actions;
using App.Gameplay.Character.Scripts.Model.Mechanics;
using App.Gameplay.LevelStorage;
using App.Gameplay.Resource;
using App.Gameplay.Resource.Model;
using Modules.Atomic.Actions;
using Modules.Atomic.Values;
using Sirenix.OdinInspector;
using UnityEngine;

namespace App.Gameplay.Character.Scripts.Model
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
        public AtomicVariable<ResourceStorage> ResourceStorage;
        public AtomicVariable<float> UnloadingDistance;
        public AtomicVariable<float> Delay;
        public AtomicVariable<bool> CanUnloadResources;

        public ResourceService ResourceService;
        public BarnService BarnService;
        public DetectionResourceAction DetectionResourceAction;
        public DetectionBarnAction DetectionBarnAction;

        //Логика
        private MovementMechanics _movementMechanics;
        private RotateMechanics _rotateMechanics;
        private DetectionResourceMechanics _detectionResourceMechanics;
        private GatheringResourceMechanics _gatheringResourceMechanics;
        private UnloadResourcesMechanics _unloadResourcesMechanics;
        private FreeSpaceResourceMechanic _freeSpaceResourceMechanic;

        private void Awake()
        {
            DetectionResourceAction = new DetectionResourceAction(this, ResourceService);
            DetectionBarnAction = new DetectionBarnAction(ResourceStorage, BarnService);
            
            _movementMechanics = new MovementMechanics(Root, MoveDirection, Speed);
            _rotateMechanics = new RotateMechanics(View, MoveDirection);
            _detectionResourceMechanics =
                new DetectionResourceMechanics(Root, TargetResource, GatheringDistance, CanGathering, IsFreeSpace, MoveDirection);
            _gatheringResourceMechanics = new GatheringResourceMechanics(TargetResource, ResourceType, GatheringCount, Amount, MaxAmount, Gathered);
            _unloadResourcesMechanics = new UnloadResourcesMechanics(ResourceStorage, CanUnloadResources, Delay, ResourceType, Amount);
            _freeSpaceResourceMechanic = new FreeSpaceResourceMechanic(IsFreeSpace, Amount, MaxAmount);
        }

        private void OnEnable()
        {
            _gatheringResourceMechanics.OnEnable();
            _freeSpaceResourceMechanic.OnEnable();
        }

        private void OnDisable()
        {
            _gatheringResourceMechanics.OnDisable();
            _freeSpaceResourceMechanic.OnDisable();
        }

        private void Update()
        {
            var deltaTime = Time.deltaTime;
            _movementMechanics.Update(deltaTime);
            _rotateMechanics.Update();
            _detectionResourceMechanics.Update();
            _unloadResourcesMechanics.Update(deltaTime);
        }
    }
}