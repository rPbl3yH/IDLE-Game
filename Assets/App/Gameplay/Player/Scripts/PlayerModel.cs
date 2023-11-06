using App.Gameplay.Character.Scripts.Model;
using App.Gameplay.LevelStorage;
using App.Gameplay.Resource;
using App.UI;
using Modules.Atomic.Actions;
using Modules.Atomic.Values;
using UnityEngine;
using VContainer;

namespace App.Gameplay.Player
{
    public class PlayerModel : MonoBehaviour
    {
        public CharacterModel CharacterModel;
        
        public ResourceStorageModelService ResourceStorageModelService;
        public ResourceService ResourceService;
        
        public AtomicEvent<ResourceType> LoadResourceSelected;
        public AtomicVariable<bool> IsShowLoadResources;

        [SerializeField] 
        private PlayerResourceViewObserver _playerResourceViewObserver;
        
        private PlayerDetectStorageMechanics _playerDetectStorageMechanics;
        private PlayerDetectionResourceMechanics _playerDetectionResourceMechanics;
        private PlayerUnloadResourceMechanics _playerUnloadResourceMechanics;
        private PlayerLoadResourceMechanics _playerLoadResourceMechanics;
        private PlayerLoadResourceAvailableMechanics _playerLoadResourceAvailableMechanics;

        [Inject]
        public void Construct(ResourceStorageModelService resourceStorageModelService, ResourceService resourceService, ResourceIconService resourceIconService)
        {
            ResourceService = resourceService;
            ResourceStorageModelService = resourceStorageModelService;
            
            CharacterModel.Construct(ResourceService);
            _playerResourceViewObserver.Construct(CharacterModel, resourceIconService);
            
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
        }

        private void Update()
        {
            _playerDetectStorageMechanics.Update();
            _playerDetectionResourceMechanics.Update();
            _playerUnloadResourceMechanics.Update();
            _playerLoadResourceAvailableMechanics.Update();
        }
    }
}