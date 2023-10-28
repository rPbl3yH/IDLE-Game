using System;
using System.Collections.Generic;
using App.Gameplay.LevelStorage;
using Modules.Atomic.Actions;
using Sirenix.OdinInspector;
using UnityEngine;

namespace App.Gameplay.Player
{
    public class BuildingModel : MonoBehaviour
    {
        public ResourceStorage ResourceStorage;
        public AtomicEvent<ResourceData> ResourceAdded;
        public Transform UnloadingPoint;

        private void OnEnable()
        {
            ResourceStorage.ResourcesChanged += OnResourcesChanged;   
        }

        private void OnDisable()
        {
            ResourceStorage.ResourcesChanged -= OnResourcesChanged;
        }

        private void OnResourcesChanged(Dictionary<ResourceType, ResourceValue> value)
        {
            if (ResourceStorage.IsFull())
            {
                
                Build();
            }
            else
            {
                print("Not full");
            }
        }

        private void Build()
        {
            print("Build");
        }
    }
}