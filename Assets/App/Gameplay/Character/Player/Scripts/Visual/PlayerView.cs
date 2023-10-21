using UnityEngine;

namespace App.Gameplay
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private PlayerModel _playerModel;
        [SerializeField] private Animator _animator;

        private PlayerAnimationController _playerAnimationController;
        
        private void Awake()
        {
            _playerAnimationController = new PlayerAnimationController(_animator, _playerModel.MoveDirection, _playerModel.CanGathering);
        }

        private void Update()
        {
            _playerAnimationController.Update();
        }
    }
}