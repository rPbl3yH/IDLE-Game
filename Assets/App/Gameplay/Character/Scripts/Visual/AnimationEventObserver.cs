using App.Gameplay.Animation;
using App.Gameplay.Character.Scripts.Model;
using Modules.Atomic.Actions;

namespace App.Gameplay.Character.Scripts.Visual
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