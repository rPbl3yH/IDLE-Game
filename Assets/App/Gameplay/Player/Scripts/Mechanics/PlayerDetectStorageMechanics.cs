using App.Gameplay.LevelStorage;
using Modules.Atomic.Values;
using UnityEngine;

namespace App.Gameplay.Player
{
    public class PlayerDetectStorageMechanics
    {
        private readonly ResourceStorageModelService _storagesService;
        private readonly AtomicVariable<ResourceStorageModel> _resourceStorageModel;
        private readonly Transform _root;

        public PlayerDetectStorageMechanics(
            ResourceStorageModelService storagesService, 
            AtomicVariable<ResourceStorageModel> resourceStorageModel, 
            Transform root)
        {
            _storagesService = storagesService;
            _resourceStorageModel = resourceStorageModel;
            _root = root;
        }

        public void Update()
        {
            _resourceStorageModel.Value = _storagesService.GetClosetModel(_root);
        }
    }
}