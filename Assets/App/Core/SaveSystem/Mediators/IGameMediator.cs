namespace App.Core.SaveSystem
{
    public interface IGameMediator
    {
        void SaveData(GameRepository gameRepository);
        void SetupData(GameRepository gameRepository);
    }
}