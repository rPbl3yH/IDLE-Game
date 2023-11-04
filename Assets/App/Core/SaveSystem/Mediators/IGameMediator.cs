namespace App.Core
{
    public interface IGameMediator
    {
        void SaveData(GameRepository gameRepository);
        void SetupData(GameRepository gameRepository);
    }
}