using App.Gameplay.Character.Scripts.Model;
using App.Gameplay.LevelStorage;

namespace App.Gameplay.Player
{
    public class PlayerDetectionBarnMechanics 
    {
        private readonly CharacterModel _characterModel;
        private readonly BarnService _barnService;

        public PlayerDetectionBarnMechanics(CharacterModel characterModel, BarnService barnService)
        {
            _characterModel = characterModel;
            _barnService = barnService;
        }

        public void Update()
        {
            _characterModel.ResourceStorage.Value = _barnService.GetStorage();
        }
    }
}