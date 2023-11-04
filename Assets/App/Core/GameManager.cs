using App.Gameplay.Character.Scripts.Model;
using VContainer;
using VContainer.Unity;

namespace App.Core
{
    public class GameManager : IStartable
    {
        [Inject] 
        private PlayerSpawner _playerSpawner;

        [Inject] 
        private GameSaver _gameSaver;
        
        public void Start()
        {
            _playerSpawner.Spawn();
        }
    }
}