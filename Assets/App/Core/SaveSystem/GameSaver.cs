using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace App.Core
{
    [Serializable]
    public class GameSaver
    {
        private readonly IEnumerable<IGameMediator> _gameMediators;
        private readonly GameRepository _gameRepository;

        public GameSaver(IEnumerable<IGameMediator> gameMediators, GameRepository gameRepository)
        {
            _gameMediators = gameMediators;
            _gameRepository = gameRepository;
        }

        [ShowInInspector]
        public void Save()
        {
            foreach (var gameMediator in _gameMediators)
            {
                gameMediator.SaveData(_gameRepository);
            }
            
            _gameRepository.SaveState();
        }

        [ShowInInspector]
        public void Load()
        {
            _gameRepository.LoadState();
        }

        [ShowInInspector]
        public void SetupData()
        {
            foreach (var gameMediator in _gameMediators)
            {
                gameMediator.SetupData(_gameRepository);    
            }
        }

        public void ClearData()
        {
            _gameRepository.ClearState();
        }
    }
}