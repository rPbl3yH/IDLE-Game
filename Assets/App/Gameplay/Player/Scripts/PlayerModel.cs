using App.Gameplay.Character.Scripts.Model;
using App.Gameplay.LevelStorage;
using UnityEngine;

namespace App.Gameplay.Player
{
    public class PlayerModel : MonoBehaviour
    {
        [SerializeField] private CharacterModel _characterModel;
        [SerializeField] private BarnService _barnService;
        
        private PlayerDetectionBarnMechanics _playerDetectionBarnMechanics;
        private PlayerDetectionResourceMechanics _playerDetectionResourceMechanics;
        private PlayerUnloadResourceMechanics _playerUnloadResourceMechanics;
        
        private void Awake()
        {
            _playerDetectionBarnMechanics = new PlayerDetectionBarnMechanics(_characterModel, _barnService);
            _playerDetectionResourceMechanics = new PlayerDetectionResourceMechanics(_characterModel);
            _playerUnloadResourceMechanics = new PlayerUnloadResourceMechanics(_characterModel, _barnService);
        }

        private void Update()
        {
            _playerDetectionBarnMechanics.Update();
            _playerDetectionResourceMechanics.Update();
            _playerUnloadResourceMechanics.Update();
        }
    }
}