using App.Gameplay.Character.Scripts.Model;
using App.Gameplay.LevelStorage;
using UnityEngine;

namespace App.Gameplay.Player
{
    public class PlayerUnloadResourceMechanics
    {
        private readonly CharacterModel _characterModel;
        private readonly ResourceStorageModelService _storagesService;

        public PlayerUnloadResourceMechanics(CharacterModel characterModel, ResourceStorageModelService storagesService)
        {
            _storagesService = storagesService;
            _characterModel = characterModel;
        }

        public void Update()
        {
            var storageModel = _storagesService.GetClosetModel(_characterModel.Root);
            _characterModel.ResourceStorage.Value = storageModel;
            
            var distance = Vector3.Distance(_characterModel.Root.position, storageModel.UnloadingPoint.position);

            if (distance <= _characterModel.UnloadingDistance.Value)
            {
                _characterModel.CanUnloadResources.Value = true;
            }
            else
            {
                _characterModel.CanUnloadResources.Value = false;
            }
        }
    }
}