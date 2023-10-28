using App.Gameplay.Animation;
using App.Gameplay.Character.Scripts.Model;
using UnityEngine;

namespace App.Gameplay.Character.Scripts.Visual
{
    public class CharacterView : MonoBehaviour
    {
        [SerializeField] private CharacterModel _characterModel;
        [SerializeField] private Animator _animator;

        [SerializeField]
        private AnimationDispatcher _animationDispatcher;
        
        private CharacterAnimationController _characterAnimationController;
        private AnimationEventObserver _animationEventObserver;
        
        private void Awake()
        {
            _characterAnimationController = new CharacterAnimationController(_animator, _characterModel.MoveDirection, _characterModel.CanGathering);
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
            _characterAnimationController.Update();
        }
    }
}