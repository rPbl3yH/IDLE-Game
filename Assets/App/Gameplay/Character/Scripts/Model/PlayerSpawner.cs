using App.Gameplay.LevelStorage;
using App.Gameplay.Player;
using App.Gameplay.Resource;
using App.Lesson;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace App.Gameplay.Character.Scripts.Model
{
    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField] private PlayerModel _playerModelPrefab;
        [SerializeField] private Transform _spawnPosition;
        [Inject] private ResourceService _resourceService;
        [Inject] private ResourceStorageModelService _resourceStorageModelService;
        [Inject] private IObjectResolver _objectResolver;

        private InputController _inputController;
        private PlayerModel _player;
        
        [Button]
        public void Spawn()
        {
            //_player = Instantiate(_playerModelPrefab, _spawnPosition.position, _spawnPosition.rotation, _spawnPosition);
            //_player.Construct(_resourceService, _resourceStorageModelService);
            _player = _objectResolver.Instantiate(_playerModelPrefab, _spawnPosition);
            _player.Construct();
        }
    }
}