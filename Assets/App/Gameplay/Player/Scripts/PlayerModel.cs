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

        private PlayerDetectionResourceMechanics _playerDetectionResourceMechanics;
        private PlayerUnloadResourceMechanics _playerUnloadResourceMechanics;
        private PlayerLoadResourceMechanics _playerLoadResourceMechanics;
        
        [SerializeField] 
        private CameraFollowingMechanics _cameraFollowingMechanics;

        public void Start()
        {
            _playerDetectionResourceMechanics = new PlayerDetectionResourceMechanics(CharacterModel);
            _playerUnloadResourceMechanics = new PlayerUnloadResourceMechanics(CharacterModel, ResourceStorageModelService);
            _playerLoadResourceMechanics = new PlayerLoadResourceMechanics(this, ResourceStorageModelService);
        }
        
        private void Update()
        {
            var deltaTime = Time.deltaTime;
            
            _playerDetectionResourceMechanics.Update();
            _playerUnloadResourceMechanics.Update();
            _playerLoadResourceMechanics.Update();
            _cameraFollowingMechanics.Update(deltaTime);
        }

        
    }
}