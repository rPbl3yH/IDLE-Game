using App.Gameplay;
using App.Gameplay.Character.Scripts.Model;
using App.Gameplay.LevelStorage;

namespace App.Core.SaveSystem.Mediators.Content
{
    public class PlayerMediator : GameMediator<CharacterData, PlayerService>
    {
        protected override void SetupFromData(PlayerService service, CharacterData data)
        {
            var player = service.GetPlayer();
            player.CharacterModel.ResourceType.Value = data.ResourceData.Type;
            player.CharacterModel.ResourceAmount.Value = data.ResourceData.Count;
        }

        protected override void SetupByDefault(PlayerService service)
        {
            var player = service.GetPlayer();
            player.CharacterModel.ResourceType.Value = ResourceType.Wood;
            player.CharacterModel.ResourceAmount.Value = 0;
        }

        protected override CharacterData ConvertToData(PlayerService service)
        {
            var characterModel = service.GetPlayer().CharacterModel;
            var data = new CharacterData
            {
                ResourceData = new ResourceData(
                    characterModel.ResourceType.Value,
                    characterModel.ResourceAmount.Value
                )
            };

            return data;
        }
    }
}