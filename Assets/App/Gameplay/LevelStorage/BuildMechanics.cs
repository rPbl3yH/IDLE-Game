using System.Collections.Generic;
using Modules.Atomic.Actions;
using UnityEngine;

namespace App.Gameplay.LevelStorage
{
    public class BuildMechanics
    {
        private readonly ResourceStorage _resourceStorage;
        private readonly IAtomicAction _built;

        public BuildMechanics(ResourceStorage resourceStorage, IAtomicAction built)
        {
            _resourceStorage = resourceStorage;
            _built = built;
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
            _built?.Invoke();
        }
    }
}