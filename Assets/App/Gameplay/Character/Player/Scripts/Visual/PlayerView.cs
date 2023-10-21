using App.Gameplay.Animation;
using UnityEngine;

namespace App.Gameplay
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private PlayerModel _playerModel;
        [SerializeField] private Animator _animator;

        [SerializeField]
        private AnimationDispatcher _animationDispatcher;
        
        private PlayerAnimationController _playerAnimationController;
        private AnimationEventObserver _animationEventObserver;
        
        private void Awake()
        {
            _playerAnimationController = new PlayerAnimationController(_animator, _playerModel.MoveDirection, _playerModel.CanGathering);
            _animationEventObserver = new AnimationEventObserver(_animationDispatcher, _playerModel);
        }

        private void OnEnable()
        {
            _animationEventObserver.OnEnable();
        }

        private void OnDisable()
        {
            _animationEventObserver.OnDisable();
        }

        private void Update()
        {
            _playerAnimationController.Update();
        }
    }
}