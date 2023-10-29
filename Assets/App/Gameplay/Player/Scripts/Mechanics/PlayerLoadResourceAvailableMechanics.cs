using System;
using App.Gameplay.Character.Scripts.Model;
using App.Gameplay.LevelStorage;
using Modules.Atomic.Values;
using UnityEngine;

namespace App.Gameplay.Player
{
    public class PlayerLoadResourceAvailableMechanics 
    {
        private readonly CharacterModel _characterModel;
        private readonly AtomicVariable<bool> _canShowLoadResources;
        private readonly ResourceStorageModelService _storagesService;
        private readonly AtomicVariable<ResourceType> _loadResourceType;

        private Transform _unloadingPoint;
        private DistanceSensor _distanceSensor;

        public PlayerLoadResourceAvailableMechanics(PlayerModel playerModel, ResourceStorageModelService storagesService)
        {
            _storagesService = storagesService;
            _characterModel = playerModel.CharacterModel;
            _loadResourceType = _characterModel.LoadResourceType;
            _canShowLoadResources = playerModel.IsShowLoadResources;
            _unloadingPoint = _storagesService.GetClosetModel(_characterModel.Root).UnloadingPoint;
            _distanceSensor = new DistanceSensor(_unloadingPoint, _characterModel.Root, _characterModel.LoadingDistance);
            
            playerModel.LoadResourceSelected.AddListener(OnLoadResourceSelected);
            _characterModel.IsFreeSpace.OnChanged += IsFreeSpaceOnChanged;
            _distanceSensor.Entered += DistanceSensorOnEntered;
            _distanceSensor.Exited += DistanceSensorOnExited;
        }

        private void DistanceSensorOnExited()
        {
            _characterModel.CanLoadResources.Value = false;
            _canShowLoadResources.Value = false;
        }

        private void DistanceSensorOnEntered()
        {
            Debug.Log("Entered");
            if (_characterModel.Amount.Value == 0)
            {
                _canShowLoadResources.Value = true;
            }
        }

        private void IsFreeSpaceOnChanged(bool value)
        {
            if (!value)
            {
                Debug.Log("Free space changed");
                _characterModel.CanLoadResources.Value = false;
            }
        }

        private void OnLoadResourceSelected(ResourceType resourceType)
        {
            _loadResourceType.Value = resourceType;
            _characterModel.CanUnloadResources.Value = false;
            _characterModel.CanLoadResources.Value = true;
        }

        public void Update()
        {
            _characterModel.ResourceStorage.Value = _storagesService.GetClosetModel(_characterModel.Root);
            if (_unloadingPoint != _characterModel.ResourceStorage.Value.UnloadingPoint)
            {
                Debug.Log("change unloadingPoint");
                _unloadingPoint = _characterModel.ResourceStorage.Value.UnloadingPoint;
            }
            _distanceSensor.Update();
        }
    }
}