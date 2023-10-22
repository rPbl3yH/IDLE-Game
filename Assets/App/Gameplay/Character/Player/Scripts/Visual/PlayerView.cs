using App.Gameplay.Animation;
using UnityEngine;

namespace App.Gameplay
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private CharacterModel _characterModel;
        [SerializeField] private Animator _animator;

        [SerializeField]
        private AnimationDispatcher _animationDispatcher;
        
        private PlayerAnimationController _playerAnimationController;
        private AnimationEventObserver _animationEventObserver;
        
        private void Awake()
        {
            _playerAnimationController = new PlayerAnimationController(_animator, _characterModel.MoveDirection, _characterModel.CanGathering);
            _animationEventObserver = new AnimationEventObserver(_animationDispatcher, _characterModel);
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