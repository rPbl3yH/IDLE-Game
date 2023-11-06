using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

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
            
            foreach (var gameMediator in _gameMediators)
            {
                // Debug.Log(gameMediator.GetType().Name);
            }
            _gameRepository = gameRepository;
        }

        [ShowInInspector]
        public void Save()
        {
            // Debug.Log("Game mediators = " + _gameMediators.Count());
            foreach (var gameMediator in _gameMediators)
            {
                gameMediator.SaveData(_gameRepository);
            }
            
            _gameRepository.SaveState();
            Debug.Log("Save");
        }

        [ShowInInspector]
        public void Load()
        {
            _gameRepository.LoadState();
            
            Debug.Log("Load");
        }

        [ShowInInspector]
        public void SetupData()
        {
            foreach (var gameMediator in _gameMediators)
            {
                gameMediator.SetupData(_gameRepository);    
            }
            
            Debug.Log("Setup data");
        }
    }
}