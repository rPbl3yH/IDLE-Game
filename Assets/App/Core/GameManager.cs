using App.Gameplay.Character.Scripts.Model;
using Modules.Tutorial;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace App.Core
{
    public class GameManager : MonoBehaviour, IStartable
    {
        [Inject] 
        private PlayerSpawner _playerSpawner;

        [Inject] 
        private GameSaver _gameSaver;

        [Inject] 
        private TutorialState _tutorialState;

        [Button]
        public void SpawnPlayer()
        {
            _playerSpawner.Spawn();
        }

        [Button]
        public void StartTutorial()
        {
            _tutorialState.NextStep();    
        }

        public void Start()
        {
            SpawnPlayer();
            StartTutorial();
        }
    }
}