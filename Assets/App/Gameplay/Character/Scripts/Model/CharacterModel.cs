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
        public AtomicVariable<int> ResourceAmount;
        public AtomicVariable<int> MaxResourceAmount;
        [ShowInInspector, ReadOnly]
        public AtomicVariable<bool> IsFreeSpace;

        [Header("Unload Resources")]
        public AtomicVariable<bool> CanUnloadResources;
        public AtomicVariable<ResourceStorageModel> ResourceStorage;
        public AtomicEvent<ResourceType> ResourceUnloaded;
        public AtomicVariable<float> UnloadingDistance;
        public AtomicVariable<float> Delay;

        [Header("Load Resources")] 
        public AtomicVariable<bool> CanLoadResources;
        public AtomicEvent<ResourceType> ResourceLoaded;
        public AtomicVariable<ResourceType> LoadResourceType;
        public AtomicVariable<float> LoadingDistance;

        public ResourceService ResourceService;
        public BarnService BarnService;
        public DetectionResourceFunction DetectionResourceFunction;
        public DetectionBarnFunction DetectionBarnFunction;

        //Логика
        private MovementMechanics _movementMechanics;
        private RotateMechanics _rotateMechanics;
        private DetectionResourceMechanics _detectionResourceMechanics;
        private GatheringResourceMechanics _gatheringResourceMechanics;
        private UnloadResourcesMechanics _unloadResourcesMechanics;
        private FreeSpaceResourceMechanic _freeSpaceResourceMechanic;
        private LoadResourceMechanics _loadResourceMechanics;
        
        private void Awake()
        {
            DetectionResourceFunction = new DetectionResourceFunction(this, ResourceService);
            DetectionBarnFunction = new DetectionBarnFunction(BarnService);
            
            _movementMechanics = new MovementMechanics(Root, MoveDirection, Speed);
            _rotateMechanics = new RotateMechanics(View, MoveDirection);
            _detectionResourceMechanics =
                new DetectionResourceMechanics(Root, TargetResource, GatheringDistance, CanGathering, IsFreeSpace, MoveDirection);
            _gatheringResourceMechanics = new GatheringResourceMechanics(TargetResource, ResourceType, GatheringCount, ResourceAmount, MaxResourceAmount, Gathered);
            _unloadResourcesMechanics = new UnloadResourcesMechanics(ResourceUnloaded, ResourceStorage, CanUnloadResources, Delay, ResourceType, ResourceAmount);
            _freeSpaceResourceMechanic = new FreeSpaceResourceMechanic(IsFreeSpace, ResourceAmount, MaxResourceAmount);
            _loadResourceMechanics = new LoadResourceMechanics(this);
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
            _loadResourceMechanics.Update(deltaTime);
        }
    }
}