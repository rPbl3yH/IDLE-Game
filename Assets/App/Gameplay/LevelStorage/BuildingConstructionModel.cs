using System;
using App.Gameplay.Building;
using Modules.Atomic.Actions;
using UnityEngine;
using VContainer;

namespace App.Gameplay.LevelStorage
{
    public class BuildingConstructionModel : ResourceStorageModel, IDisposable
    {
        public AtomicEvent<BuildingModel> Built = new();
        public GameObject SpawnPoint;
        public BuildingModel BuildingModel;
        
        public ResourceStorageModelService ResourceStorageModelService;

        private BuildConstructionMechanics _buildConstructionMechanics;
        private BuildObserverMechanics _buildObserverMechanics;
        
        [Inject]
        public void Construct(ResourceStorageModelService resourceStorageModelService, BuildingSpawner buildingSpawner)
        {
            ResourceStorageModelService = resourceStorageModelService;
            _buildConstructionMechanics = new BuildConstructionMechanics(ResourceStorage, Built, buildingSpawner, BuildingModel, SpawnPoint.transform);
            _buildObserverMechanics = new BuildObserverMechanics(this, Built, ResourceStorageModelService, SpawnPoint);
            
            Built.AddListener(OnBuilt);            
            
            _buildConstructionMechanics.OnEnable();
            _buildObserverMechanics.OnEnable();
        }

        private void OnBuilt(BuildingModel obj)
        {
            Destroy(gameObject);
        }

        public void Dispose()
        {
            _buildConstructionMechanics.OnDisable();
            _buildObserverMechanics.OnDisable();
        }
    }
}