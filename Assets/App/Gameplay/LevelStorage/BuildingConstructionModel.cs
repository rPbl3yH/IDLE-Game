﻿using System;
using App.Gameplay.Building;
using App.Gameplay.Building.Barn;
using Atomic.Elements;
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
        private BarnRegisterMechanics _barnRegisterMechanics;
        private DeactivateViewMechanics _deactivateViewMechanics;
        
        [Inject]
        public void Construct(ResourceStorageModelService resourceStorageModelService, BuildingSpawner buildingSpawner, BarnModelService barnModelService)
        {
            ResourceStorageModelService = resourceStorageModelService;
            _buildConstructionMechanics = new BuildConstructionMechanics(ResourceStorage, Built, buildingSpawner, BuildingModel, SpawnPoint.transform);
            _buildObserverMechanics = new BuildObserverMechanics(this, Built, ResourceStorageModelService, SpawnPoint);
            _barnRegisterMechanics = new BarnRegisterMechanics(Built, barnModelService);
            
            Built.Subscribe(OnBuilt);            
            
            //TODO: to initialize method
            _buildConstructionMechanics.OnEnable();
            _buildObserverMechanics.OnEnable();
            _barnRegisterMechanics.OnEnable();
        }

        private void OnBuilt(BuildingModel obj)
        {
            Destroy(gameObject);
        }

        public void Dispose()
        {
            _buildConstructionMechanics.OnDisable();
            _buildObserverMechanics.OnDisable();
            _barnRegisterMechanics.OnDisable();
        }
    }
}