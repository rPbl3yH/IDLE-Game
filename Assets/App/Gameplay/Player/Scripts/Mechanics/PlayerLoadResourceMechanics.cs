using System;
using App.Gameplay.Character.Scripts.Model;
using App.Gameplay.LevelStorage;
using Modules.Atomic.Values;
using UnityEngine;

namespace App.Gameplay.Player
{
    public class PlayerLoadResourceMechanics : IDisposable
    {
        private readonly CharacterModel _characterModel;
        private readonly AtomicVariable<bool> _canShowLoadResources;
        private readonly ResourceStorageModelService _storagesService;
        private readonly AtomicVariable<ResourceType> _loadResourceType;

        public PlayerLoadResourceMechanics(PlayerModel playerModel, ResourceStorageModelService storagesService)
        {
            _storagesService = storagesService;
            _characterModel = playerModel.CharacterModel;
            _loadResourceType = _characterModel.LoadResourceType;
            _canShowLoadResources = playerModel.IsShowLoadResources;
            playerModel.LoadResourceSelected.AddListener(OnLoadResourceSelected);
            // _characterModel.ResourceLoaded.AddListener(OnResourceLoaded);
        }

        private void OnLoadResourceSelected(ResourceType resourceType)
        {
            _loadResourceType.Value = resourceType;
            _characterModel.CanLoadResources.Value = true;
        }

        public void Update()
        {
            
            var storageModel = _storagesService.GetClosetModel(_characterModel.Root);
            _characterModel.ResourceStorage.Value = storageModel;
            
            var distance = Vector3.Distance(_characterModel.Root.position, storageModel.UnloadingPoint.position);

            if (distance <= _characterModel.LoadingDistance.Value && _characterModel.Amount.Value == 0)
            {
                _canShowLoadResources.Value = true;
            }
            else
            {
                _canShowLoadResources.Value = false;
            }
        }


        public void Dispose()
        {
            // _characterModel.ResourceLoaded.RemoveListener(OnResourceLoaded);
        }
    }
}