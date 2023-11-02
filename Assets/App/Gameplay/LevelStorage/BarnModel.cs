using System;
using App.Gameplay.Character.Scripts.Model;
using App.Gameplay.Player;
using Modules.Atomic.Actions;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;

namespace App.Gameplay.LevelStorage
{
    public class BarnModel : ResourceStorageModel
    {
        public AtomicEvent<ResourceType> ResourceLoaded;
        public AtomicEvent<ResourceType> ResourceSelected;

        [SerializeField]
        private BarnModelObserver _barnModelObserver;

        [Inject]
        public void Construct(PlayerService playerService)
        {
            _barnModelObserver.Construct(playerService.GetPlayer());
        }
        
        [Button]
        public void SelectLoadResource(ResourceType resourceType)
        {
            ResourceSelected?.Invoke(resourceType);
        }
    }
}