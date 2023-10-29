using System;
using App.Gameplay.Character.Scripts.Model;
using Modules.Atomic.Values;
using UnityEngine;

namespace App.Gameplay.Player
{
    public class PlayerLoadResourceAvailableMechanics 
    {
        private readonly CharacterModel _characterModel;
        private readonly AtomicVariable<bool> _canShowLoadResources;
        private readonly AtomicVariable<ResourceType> _loadResourceType;
        private readonly DistanceSensor _distanceSensor;

        public PlayerLoadResourceAvailableMechanics(PlayerModel playerModel)
        {
            _characterModel = playerModel.CharacterModel;
            _loadResourceType = _characterModel.LoadResourceType;
            _canShowLoadResources = playerModel.IsShowLoadResources;
            _distanceSensor = new DistanceSensor(_characterModel.LoadingDistance);
            
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
            _distanceSensor.SetPoints(_characterModel.Root, _characterModel.ResourceStorage.Value.UnloadingPoint);
            _distanceSensor.Update();
        }
    }
}