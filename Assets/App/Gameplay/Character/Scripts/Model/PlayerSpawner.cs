using System;
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
        public event Action<PlayerModel> Spawned; 

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
            _player = _objectResolver.Instantiate(_playerModelPrefab, _spawnPosition);
            _player.Construct();
            Spawned?.Invoke(_player);
        }
    }
}