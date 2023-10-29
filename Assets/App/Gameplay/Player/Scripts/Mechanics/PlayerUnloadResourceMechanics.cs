using App.Gameplay.Character.Scripts.Model;
using App.Gameplay.LevelStorage;
using UnityEngine;

namespace App.Gameplay.Player
{
    public class PlayerUnloadResourceMechanics
    {
        private readonly CharacterModel _characterModel;
        private readonly ResourceStorageModelService _storagesService;
        private readonly DistanceSensor _distanceSensor;
        private Transform _unloadingPoint;

        public PlayerUnloadResourceMechanics(CharacterModel characterModel, ResourceStorageModelService storagesService)
        {
            _storagesService = storagesService;
            _characterModel = characterModel;
            _unloadingPoint = _storagesService.GetClosetModel(_characterModel.Root).UnloadingPoint;
            _distanceSensor = new DistanceSensor(_characterModel.Root, _unloadingPoint, _characterModel.UnloadingDistance);
            _distanceSensor.Entered += DistanceSensorOnEntered;
            _distanceSensor.Exited += DistanceSensorOnExited;
        }

        private void DistanceSensorOnExited()
        {
            _characterModel.CanUnloadResources.Value = false;
        }

        private void DistanceSensorOnEntered()
        {
            _characterModel.CanUnloadResources.Value = true;
        }

        public void Update()
        {
            _unloadingPoint = _storagesService.GetClosetModel(_characterModel.Root).UnloadingPoint;
            _distanceSensor.Update();
        }
    }
}