using App.Gameplay.Character.Scripts.Model;
using App.Gameplay.Player;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace App.Meta
{
    public class PlayerPointerController : IInitializable, ITickable
    {
        [Inject]
        private FloorPointer _pointer;

        [Inject] 
        private PlayerSpawner _playerSpawner;

        private Vector3 _targetPosition;
        private Transform _root;

        private bool _isEnable;

        void IInitializable.Initialize()
        {
            _playerSpawner.Spawned += PlayerSpawnerOnSpawned;
        }

        void ITickable.Tick()
        {
            if (!_isEnable)
            {
                return;
            }
            
            var delta = _targetPosition - _root.position;
            _pointer.transform.rotation = Quaternion.LookRotation(delta.normalized);
        }

        private void PlayerSpawnerOnSpawned(PlayerModel player)
        {
            _root = player.CharacterModel.Root;
            _pointer.transform.SetParent(_root);
        }

        public void SetTarget(Transform point)
        {
            if (point == null || _root == null)
            {
                _pointer.gameObject.SetActive(false);
                _isEnable = false;
                return;
            }

            _pointer.gameObject.SetActive(true);
            _targetPosition = point.position;
            _isEnable = true;
        }
    }
}
