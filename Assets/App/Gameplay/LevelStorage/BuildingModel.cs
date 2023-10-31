using System;
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
        public void Construct(ResourceStorageModelService resourceStorageModelService)
        {
            ResourceStorageModelService = resourceStorageModelService;
            var parent = ResourceStorageModelService.transform;
            _buildMechanics = new BuildMechanics(ResourceStorage, Built, Building, SpawnPoint.transform, parent);
            _buildObserverMechanics = new BuildObserverMechanics(this, Built, ResourceStorageModelService, SpawnPoint);
            
            _buildMechanics.OnEnable();
            _buildObserverMechanics.OnEnable();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}