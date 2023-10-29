using System;
using App.Gameplay.Player;
using Modules.Atomic.Actions;
using Sirenix.OdinInspector;
using UnityEngine;

namespace App.Gameplay.LevelStorage
{
    public class BarnModel : ResourceStorageModel
    {
        public AtomicEvent<ResourceType> ResourceSelected;

        [Button]
        public void SelectLoadResource(ResourceType resourceType)
        {
            ResourceSelected?.Invoke(resourceType);
        }
    }
}