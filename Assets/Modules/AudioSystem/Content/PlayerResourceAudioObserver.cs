using App.Gameplay.Character.Scripts.Model;
using App.Gameplay.Player;
using Modules.Atomic.Values;
using UnityEngine;
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

        private IAtomicVariable<int> _resourceAmount;
        private int _amount;

        void IInitializable.Initialize()
        {
            _playerSpawner.Spawned += PlayerSpawnerOnSpawned;
        }

        private void PlayerSpawnerOnSpawned(PlayerModel player)
        {
            _playerSpawner.Spawned -= PlayerSpawnerOnSpawned;
            
            _resourceAmount = player.CharacterModel.ResourceAmount;
            _resourceAmount.OnChanged += ResourceAmountOnOnChanged;
        }

        private void ResourceAmountOnOnChanged(int value)
        {
            var difference = Mathf.Abs(_amount - value);
            
            if (difference > 0)
            {
                _gameSoundManager.PlayAudio(GameSoundType.TransferResource);
                _amount = value;
            }
        }
    }
}