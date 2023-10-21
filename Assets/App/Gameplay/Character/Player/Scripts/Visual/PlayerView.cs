using System;
using App.Gameplay.Animation;
using Atomic;
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
            _animationEventObserver = new AnimationEventObserver(_animationDispatcher, _playerModel.Gathered);
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

    public class AnimationEventObserver
    {
        private readonly AnimationDispatcher _animationDispatcher;
        private readonly AtomicEvent _gathered;

        public AnimationEventObserver(AnimationDispatcher animationDispatcher, AtomicEvent gathered)
        {
            _animationDispatcher = animationDispatcher;
            _gathered = gathered;
        }

        public void OnEnable()
        {
            _animationDispatcher.EventRequested += OnEventRequested;
        }

        public void OnDisable()
        {
            _animationDispatcher.EventRequested -= OnEventRequested;
        }

        private void OnEventRequested(string eventRequest)
        {
            if (eventRequest == "Gathering")
            {
                _gathered?.Invoke();
            }
        }
    }
}