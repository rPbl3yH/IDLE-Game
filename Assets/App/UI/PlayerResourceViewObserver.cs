using System;
using App.Gameplay.Character.Scripts.Model;
using App.Gameplay.Player;
using App.UI.UIManager;
using UnityEngine;
using VContainer;

namespace App.UI
{
    public class PlayerResourceViewObserver : MonoBehaviour, IDisposable
    {
        private CharacterModel _characterModel;
        private ResourceView _resourceViewPrefab;

        private PlayerSpawner _playerSpawner;
        private ResourceView _resourceView;
        private UISpawnService _uiSpawnService;

        [Inject]
        public void Construct(PlayerSpawner playerSpawner, ResourceView resourceView, UISpawnService spawnService)
        {
            Debug.Log("spawn player resource observer");
            _uiSpawnService = spawnService;
            _playerSpawner = playerSpawner;
            _playerSpawner.Spawned += PlayerSpawnerOnSpawned;
            _resourceViewPrefab = resourceView;
        }

        private void PlayerSpawnerOnSpawned(PlayerModel playerModel)
        {
            _playerSpawner.Spawned -= PlayerSpawnerOnSpawned;
            
            _characterModel = playerModel.CharacterModel;
            _resourceView = _uiSpawnService.Spawn(_resourceViewPrefab);
            
            OnResourceAmountChanged(_characterModel.ResourceAmount.Value);
            _characterModel.ResourceAmount.OnChanged += OnResourceAmountChanged;
        }

        private void OnResourceAmountChanged(int value)
        {
            if (value > 0)
            {
                _resourceView.gameObject.SetActive(true);
                var text = $"{_characterModel.ResourceType.Value} {value}";
                _resourceView.Show(text);    
            }
            else
            {
                _resourceView.Hide();
            }
        }

        public void Dispose()
        {
            _characterModel.ResourceAmount.OnChanged -= OnResourceAmountChanged;
        }
    }
}