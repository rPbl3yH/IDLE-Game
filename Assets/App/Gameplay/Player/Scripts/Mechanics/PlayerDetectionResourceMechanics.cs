using App.Gameplay.Character.Scripts.Model;

namespace App.Gameplay.Player
{
    public class PlayerDetectionResourceMechanics 
    {
        private readonly CharacterModel _characterModel;

        public PlayerDetectionResourceMechanics(CharacterModel characterModel)
        {
            _characterModel = characterModel;
        }

        public void Update()
        {
            _characterModel.DetectionResourceAction?.Invoke();
        }
    }
}