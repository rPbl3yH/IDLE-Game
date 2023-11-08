using System;
using App.UI.UIManager;
using UnityEngine;
using UnityEngine.UI;

namespace App.UI.Panel
{
    public class GameMenuPanel : BaseUIView
    {
        public event Action ResetProgressButtonClicked;
        public event Action ExitButtonClicked;
        public event Action CloseButtonClicked;
        
        [SerializeField] private Button _resetProgressButton;
        [SerializeField] private Button _exitButton;
        [SerializeField] private Button _closeButton;

        private void OnEnable()
        {
            _resetProgressButton.onClick.AddListener(OnResetButtonClicked);
            _exitButton.onClick.AddListener(OnExitButtonClicked);
            _closeButton.onClick.AddListener(OnCloseButtonClicked);
        }

        private void OnDisable()
        {
            _resetProgressButton.onClick.RemoveListener(OnResetButtonClicked);
            _exitButton.onClick.RemoveListener(OnExitButtonClicked);
            _closeButton.onClick.RemoveListener(OnCloseButtonClicked);
        }

        private void OnCloseButtonClicked()
        {
            CloseButtonClicked?.Invoke();
        }

        private void OnResetButtonClicked()
        {
            ResetProgressButtonClicked?.Invoke();
        }

        private void OnExitButtonClicked()
        {
            ExitButtonClicked?.Invoke();
        }
    }
}