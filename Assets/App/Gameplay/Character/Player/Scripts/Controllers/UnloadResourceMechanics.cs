using App.Gameplay.LevelStorage;
using UnityEngine;

namespace App.Gameplay
{
    public class UnloadResourceMechanics
    {
        private readonly CharacterModel _characterModel;
        private readonly BarnService _barnService;

        public UnloadResourceMechanics(CharacterModel characterModel, BarnService barnService)
        {
            _barnService = barnService;
            _characterModel = characterModel;
        }

        public void Update()
        {
            var barn = _barnService.GetStorage();
            var distance = Vector3.Distance(_characterModel.Root.position, barn.UnloadingPoint.position);

            if (distance <= _characterModel.UnloadingDistance.Value)
            {
                _characterModel.CanUnloadResources.Value = true;
            }
            else
            {
                _characterModel.CanUnloadResources.Value = false;
            }
        }
    }
}