using App.Gameplay.Player;

namespace App.Gameplay.Character.Scripts.Model
{
    public class PlayerService
    {
        private PlayerModel _playerModel;
        
        public void SetPlayer(PlayerModel playerModel)
        {
            _playerModel = playerModel;
        }
        
        public PlayerModel GetPlayer()
        {
            return _playerModel;
        }
    }
}