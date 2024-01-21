using App.Gameplay;
using App.Gameplay.Character.Scripts.Model;
using App.Gameplay.Player;
using VContainer;
using VContainer.Unity;

namespace Modules.AudioSystem.Content
{
    public class PlayerResourceAudioObserver : IInitializable
    {
        [Inject] 
        private GameSoundManager _gameSoundManager;

        [Inject] 
        private PlayerSpawner _playerSpawner;
        
        private int _amount;

        void IInitializable.Initialize()
        {
            _playerSpawner.Spawned += PlayerSpawnerOnSpawned;
        }

        private void PlayerSpawnerOnSpawned(PlayerModel player)
        {
            _playerSpawner.Spawned -= PlayerSpawnerOnSpawned;
            
            player.CharacterModel.ResourceLoaded.Subscribe(OnTransferred);
            player.CharacterModel.ResourceUnloaded.Subscribe(OnTransferred);
        }

        private void OnTransferred(ResourceType resourceType)
        {
            _gameSoundManager.PlayAudio(GameSoundType.TransferResource);
        }
    }
}