using App.Gameplay.Character.Scripts.Model;
using App.Gameplay.Resource.Model;
using Modules.Atomic.Actions;

namespace App.Gameplay.Player
{
    public class PlayerDetectionResourceMechanics 
    {
        private readonly CharacterModel _characterModel;
        private readonly IAtomicFunction<ResourceModel> _resourceFunction;

        public PlayerDetectionResourceMechanics(CharacterModel characterModel)
        {
            _characterModel = characterModel;
            _resourceFunction = _characterModel.DetectionResourceFunction;
        }

        public void Update()
        {
            _characterModel.TargetResource.Value = _resourceFunction?.GetResult();
        }
    }
}