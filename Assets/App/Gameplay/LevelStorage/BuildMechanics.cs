using System.Collections.Generic;
using Modules.Atomic.Actions;
using UnityEngine;

namespace App.Gameplay.LevelStorage
{
    public class BuildMechanics
    {
        private readonly ResourceStorage _resourceStorage;
        private readonly Transform _spawnPoint;
        private readonly Transform _parent;
        private readonly IAtomicAction<Building> _built;
        private readonly Building _building;
        
        public BuildMechanics(ResourceStorage resourceStorage, IAtomicAction<Building> built, Building building, Transform spawnPoint, Transform parent)
        {
            _resourceStorage = resourceStorage;
            _built = built;
            _building = building;
            _spawnPoint = spawnPoint;
            _parent = parent;
        }

        public void OnEnable()
        {
            _resourceStorage.ResourcesChanged += OnResourcesChanged;   
        }

        public void OnDisable()
        {
            _resourceStorage.ResourcesChanged -= OnResourcesChanged;
        }

        private void OnResourcesChanged(Dictionary<ResourceType, ResourceValue> value)
        {
            if (_resourceStorage.IsFull())
            {
                Build();
            }
        }

        private void Build()
        {
            var building = Object.Instantiate(_building, _spawnPoint.position, _spawnPoint.rotation, _parent);
            _built?.Invoke(building);
        }
    }
}