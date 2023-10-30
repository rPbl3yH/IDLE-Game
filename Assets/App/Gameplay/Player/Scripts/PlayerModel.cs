using App.GameEngine.Input.Handlers;
using App.Gameplay.Character.Scripts.Model;
using App.Gameplay.LevelStorage;
using App.Gameplay.Resource;
using Modules.Atomic.Actions;
using Modules.Atomic.Values;
using UnityEngine;
using VContainer;

namespace App.Gameplay.Player
{
    public class PlayerModel : MonoBehaviour
    {
        public CharacterModel CharacterModel;
        
        [Inject]
        public ResourceStorageModelService ResourceStorageModelService;

        public AtomicEvent<ResourceType> LoadResourceSelected;
        public AtomicVariable<bool> IsShowLoadResources;

        private PlayerDetectStorageMechanics _playerDetectStorageMechanics;
        private PlayerDetectionResourceMechanics _playerDetectionResourceMechanics;
        private PlayerUnloadResourceMechanics _playerUnloadResourceMechanics;
        private PlayerLoadResourceMechanics _playerLoadResourceMechanics;
        private PlayerLoadResourceAvailableMechanics _playerLoadResourceAvailableMechanics;
        
        [Inject] private IInputHandler _inputHandler;
        [Inject] private ResourceService _resourceService;
        
        [SerializeField] 
        private CameraFollowingMechanics _cameraFollowingMechanics;

        public void Construct()
        {
            CharacterModel.Construct(_resourceService);
            
            _playerDetectStorageMechanics = new PlayerDetectStorageMechanics(ResourceStorageModelService, CharacterModel.ResourceStorage, CharacterModel.Root);
            _playerDetectionResourceMechanics = new PlayerDetectionResourceMechanics(CharacterModel);
            
            //TODO: заменить с VContainer
            _playerUnloadResourceMechanics = new PlayerUnloadResourceMechanics(CharacterModel.UnloadingDistance,
                CharacterModel.CanUnloadResources, CharacterModel.ResourceStorage, CharacterModel.Root);
            _playerUnloadResourceMechanics.OnEnable();

            _playerLoadResourceMechanics = new PlayerLoadResourceMechanics(LoadResourceSelected,
                CharacterModel.IsFreeSpace, CharacterModel.CanUnloadResources, CharacterModel.LoadResourceType,
                CharacterModel.CanLoadResources);
            _playerLoadResourceMechanics.OnEnable();
            
            _playerLoadResourceAvailableMechanics = new PlayerLoadResourceAvailableMechanics(this);
            _playerLoadResourceAvailableMechanics.OnEnable();
            
            _inputHandler.DirectionChanged += OnDirectionChanged;
        }

        private void OnDirectionChanged(Vector3 modeDirection)
        {
            CharacterModel.MoveDirection.Value = modeDirection;
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