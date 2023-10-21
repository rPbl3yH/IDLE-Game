using App.Gameplay.Animation;
using Atomic;

namespace App.Gameplay
{
    public class AnimationEventObserver
    {
        private readonly AnimationDispatcher _animationDispatcher;
        private readonly AtomicEvent _gathered;

        public AnimationEventObserver(AnimationDispatcher animationDispatcher, PlayerModel playerModel)
        {
            _animationDispatcher = animationDispatcher;
            _gathered = playerModel.Gathered;
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