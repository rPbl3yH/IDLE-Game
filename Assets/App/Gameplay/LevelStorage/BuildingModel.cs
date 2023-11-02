using System;
using App.Gameplay.Building;
using Modules.Atomic.Actions;
using UnityEngine;
using VContainer;

namespace App.Gameplay.LevelStorage
{
    public class BuildingModel : ResourceStorageModel, IDisposable
    {
        public AtomicEvent<Building> Built = new();
        public GameObject SpawnPoint;
        public Building Building;
        
        public ResourceStorageModelService ResourceStorageModelService;

        private BuildMechanics _buildMechanics;
        private BuildObserverMechanics _buildObserverMechanics;
        
        [Inject]
        public void Construct(ResourceStorageModelService resourceStorageModelService, BuildingSpawner buildingSpawner)
        {
            ResourceStorageModelService = resourceStorageModelService;
            _buildMechanics = new BuildMechanics(ResourceStorage, Built, buildingSpawner, Building, SpawnPoint.transform);
            _buildObserverMechanics = new BuildObserverMechanics(this, Built, ResourceStorageModelService, SpawnPoint);
            
            _buildMechanics.OnEnable();
            _buildObserverMechanics.OnEnable();
        }

        public void Dispose()
        {
            _buildMechanics.OnDisable();
            _buildObserverMechanics.OnDisable();
        }
    }
}