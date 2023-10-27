using App.Gameplay.Resource;
using UnityEngine;

namespace App.Gameplay
{
    public class ResourceDetectMechanics 
    {
        private readonly CharacterModel _characterModel;

        public ResourceDetectMechanics(CharacterModel characterModel)
        {
            _characterModel = characterModel;
        }

        public void Update()
        {
            _characterModel.DetectionResourceAction?.Invoke();
        }
    }
}