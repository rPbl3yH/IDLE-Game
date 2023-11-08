using App.Gameplay.LevelStorage;

namespace App.Gameplay.Building
{
    public class BuildingConstructionService : GameService<BuildingConstructionModel>
    {
        public void HideAllConstruction()
        {
            foreach (var constructionModel in Services)
            {
                constructionModel.Deactivated?.Invoke();
            }
        }

        public void ShowAll()
        {
            foreach (var constructionModel in Services)
            {
                constructionModel.IsEnable.Value = true;
            }
        }
    }
}