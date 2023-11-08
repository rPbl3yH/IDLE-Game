using System;
using App.Core;
using UnityEngine;
using VContainer.Unity;

namespace App.UI.Panel
{
    public class GameMenuPanelObserver : IDisposable, IInitializable
    {
        private readonly GameMenuButton _gameMenuButton;
        private readonly GameMenuPanel _gameMenuPanel;
        private readonly SceneLoader _sceneLoader;
        private readonly GameSaver _gameSaver;
        
        public GameMenuPanelObserver(
            GameMenuButton gameMenuButton, 
            GameMenuPanel gameMenuPanel, 
            SceneLoader sceneLoader, 
            GameSaver gameSaver)
        {
            _gameMenuButton = gameMenuButton;
            _gameMenuPanel = gameMenuPanel;
            _sceneLoader = sceneLoader;
            _gameSaver = gameSaver;
        }

        public void Initialize()
        {
            _gameMenuButton.Clicked += GameMenuButtonOnClicked;
            _gameMenuPanel.ResetProgressButtonClicked += OnResetProgressButtonClicked;
            _gameMenuPanel.ExitButtonClicked += OnExitButtonClicked;
            _gameMenuPanel.CloseButtonClicked += OnCloseButtonClicked;
        }
        
        private void OnCloseButtonClicked()
        {
            _gameMenuPanel.Hide();
        }

        private void GameMenuButtonOnClicked()
        {
            _gameMenuPanel.Show();
        }

        private void OnExitButtonClicked()
        {
            _gameSaver.Save();
            Application.Quit();
        }

        private void OnResetProgressButtonClicked()
        {
            _gameMenuPanel.Hide();
            _gameSaver.ClearData();
            _sceneLoader.LoadLoadingScene();
        }

        public void Dispose()
        {
            _gameMenuPanel.ResetProgressButtonClicked -= OnResetProgressButtonClicked;
            _gameMenuPanel.ExitButtonClicked -= OnExitButtonClicked;
            _gameMenuButton.Clicked -= GameMenuButtonOnClicked;
            _gameMenuPanel.CloseButtonClicked -= OnCloseButtonClicked;
        }
    }
}