using System;
using App.Gameplay.LevelStorage;
using UnityEngine;

namespace App.Gameplay
{
    public class BarnDetectMechanics 
    {
        private readonly CharacterModel _characterModel;
        private readonly BarnService _barnService;

        public BarnDetectMechanics(CharacterModel characterModel, BarnService barnService)
        {
            _characterModel = characterModel;
            _barnService = barnService;
        }

        public void Update()
        {
            _characterModel.LevelStorage.Value = _barnService.GetStorage();
        }
    }
}