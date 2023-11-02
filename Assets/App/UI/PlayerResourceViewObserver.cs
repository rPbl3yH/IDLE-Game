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
        [SerializeField]
        private CharacterModel _characterModel;

        [SerializeField]
        private ResourceView _resourceView;

        private PlayerSpawner _playerSpawner;
        private UISpawnService _uiSpawnService;

        public void Construct(CharacterModel characterModel)
        {
            _characterModel = characterModel;
            
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