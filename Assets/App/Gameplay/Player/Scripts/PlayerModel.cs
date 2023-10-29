using System;
using App.Gameplay.Character.Scripts.Model;
using App.Gameplay.LevelStorage;
using Modules.Atomic.Actions;
using Modules.Atomic.Values;
using UnityEngine;
using VContainer.Unity;

namespace App.Gameplay.Player
{
    public class PlayerModel : MonoBehaviour
    {
        public CharacterModel CharacterModel;
        public ResourceStorageModelService ResourceStorageModelService;

        public AtomicEvent<ResourceType> LoadResourceSelected;
        public AtomicVariable<bool> IsShowLoadResources;

        private PlayerDetectStorageMechanics _playerDetectStorageMechanics;
        private PlayerDetectionResourceMechanics _playerDetectionResourceMechanics;
        private PlayerUnloadResourceMechanics _playerUnloadResourceMechanics;
        private PlayerLoadResourceAvailableMechanics _playerLoadResourceAvailableMechanics;
        
        [SerializeField] 
        private CameraFollowingMechanics _cameraFollowingMechanics;

        public void Start()
        {
            _playerDetectStorageMechanics = new PlayerDetectStorageMechanics(ResourceStorageModelService, CharacterModel.ResourceStorage, CharacterModel.Root);
            _playerDetectionResourceMechanics = new PlayerDetectionResourceMechanics(CharacterModel);
            
            //TODO: заменить с VContainer
            _playerUnloadResourceMechanics = new PlayerUnloadResourceMechanics(CharacterModel);
            _playerUnloadResourceMechanics.OnEnable();
            
            _playerLoadResourceAvailableMechanics = new PlayerLoadResourceAvailableMechanics(this);
            _playerLoadResourceAvailableMechanics.OnEnable();
        }

        private void Update()
        {
            var deltaTime = Time.deltaTime;
            _playerDetectStorageMechanics.Update();
            _playerDetectionResourceMechanics.Update();
            _playerUnloadResourceMechanics.Update();
            _playerLoadResourceAvailableMechanics.Update();
            _cameraFollowingMechanics.Update(deltaTime);
        }
    }
}