using UnityEngine;

namespace App.Gameplay
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private Animator _animator;

        private PlayerAnimationController _playerAnimationController;
        
        private void Awake()
        {
            _playerAnimationController = new PlayerAnimationController(_player.MoveDirection, _animator);
        }

        private void Update()
        {
            _playerAnimationController.Update();
        }
    }
}