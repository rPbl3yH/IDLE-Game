using System;
using Modules.Atomic.Actions;
using UnityEngine;

namespace App.Gameplay.LevelStorage
{
    public class BuildingModel : ResourceStorageModel
    {
        public AtomicEvent<Building> Built = new();
        public GameObject SpawnPoint;
        public Building Building;
        public GameObject BuildingUI;
        public ResourceStorageModelService ResourceStorageModelService;

        private BuildMechanics _buildMechanics;
        private BuildObserverMechanics _buildObserverMechanics;
        
        private void Awake()
        {
            var parent = ResourceStorageModelService.transform;
            _buildMechanics = new BuildMechanics(ResourceStorage, Built, Building, SpawnPoint.transform, parent);
            _buildObserverMechanics = new BuildObserverMechanics(this, Built, ResourceStorageModelService, SpawnPoint, BuildingUI);
        }

        private void OnEnable()
        {
            _buildMechanics.OnEnable();
            _buildObserverMechanics.OnEnable();
        }

        private void OnDisable()
        {
            _buildMechanics.OnDisable();
            _buildObserverMechanics.OnDisable();
        }
    }
}