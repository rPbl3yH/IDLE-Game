using System;
using Modules.Atomic.Actions;
using UnityEngine;

namespace App.Gameplay.LevelStorage
{
    public class BuildingModel : ResourceStorageModel
    {
        public AtomicEvent Built;
        public Transform SpawnPoint;
        public GameObject Prefab;

        private BuildMechanics _buildMechanics;
        private BuildObjectMechanics _buildObjectMechanics;

        private void Awake()
        {
            _buildMechanics = new BuildMechanics(ResourceStorage, Built);
            _buildObjectMechanics = new BuildObjectMechanics(Built, Prefab, SpawnPoint);
        }

        private void OnEnable()
        {
            _buildMechanics.OnEnable();
            _buildObjectMechanics.OnEnable();
        }

        private void OnDisable()
        {
            _buildMechanics.OnDisable();
            _buildObjectMechanics.OnDisable();
        }
    }
}