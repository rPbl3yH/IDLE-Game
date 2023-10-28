using App.Gameplay.Character.Scripts.Model;
using App.Gameplay.LevelStorage;
using UnityEngine;

namespace App.Gameplay.Player
{
    public class PlayerModel : MonoBehaviour
    {
        [SerializeField] private CharacterModel _characterModel;
        [SerializeField] private BarnService _barnService;
        [SerializeField] private ResourceStorageModelService _resourceStorageModelService; 
        
        private PlayerDetectionBarnMechanics _playerDetectionBarnMechanics;
        private PlayerDetectionResourceMechanics _playerDetectionResourceMechanics;
        private PlayerUnloadResourceMechanics _playerUnloadResourceMechanics;

        [SerializeField] 
        private CameraFollowingMechanics _cameraFollowingMechanics;
        
        private void Awake()
        {
            _playerDetectionBarnMechanics = new PlayerDetectionBarnMechanics(_characterModel, _barnService);
            _playerDetectionResourceMechanics = new PlayerDetectionResourceMechanics(_characterModel);
            _playerUnloadResourceMechanics = new PlayerUnloadResourceMechanics(_characterModel, _resourceStorageModelService);
        }

        private void Update()
        {
            var deltaTime = Time.deltaTime;
            
            _playerDetectionBarnMechanics.Update();
            _playerDetectionResourceMechanics.Update();
            _playerUnloadResourceMechanics.Update();
            _cameraFollowingMechanics.Update(deltaTime);
        }
    }
}