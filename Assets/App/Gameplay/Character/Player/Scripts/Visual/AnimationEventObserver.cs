using App.Gameplay.Animation;
using Atomic;

namespace App.Gameplay
{
    public class AnimationEventObserver
    {
        private readonly AnimationDispatcher _animationDispatcher;
        private readonly IAtomicAction _gathered;

        public AnimationEventObserver(AnimationDispatcher animationDispatcher, CharacterModel characterModel)
        {
            _animationDispatcher = animationDispatcher;
            _gathered = characterModel.Gathered;
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