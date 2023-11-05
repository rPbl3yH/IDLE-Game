using System.Linq;
using App.Gameplay;
using App.Gameplay.Building;
using App.Gameplay.LevelStorage;
using VContainer.Unity;

namespace Modules.Tutorial.Content
{
    public class BuildBarn_TutorialStep : IInitializable
    {
        private readonly BuildingConstructionService _buildingConstructionService;
        private readonly TutorialState _tutorialState;


        public BuildBarn_TutorialStep(BuildingConstructionService buildingConstructionService, TutorialState tutorialState)
        {
            _buildingConstructionService = buildingConstructionService;
            _tutorialState = tutorialState;
        }

        public void Initialize()
        {
            _tutorialState.StepStarted += TutorialStateOnStepStarted;
        }

        private void TutorialStateOnStepStarted(TutorialStep tutorialStep)
        {
            if (tutorialStep == TutorialStep.BuildBarn)
            {
                Show();
            }
        }

        private void Show()
        {
            var services = _buildingConstructionService.GetServices();
            var model = services.FirstOrDefault(model => model.BuildingModel is BarnModel);

            if (model != null)
            {
                model.IsEnable.Value = true;
                model.Built.AddListener(OnBuilt);
            }
        }

        private void OnBuilt(BuildingModel buildingModel)
        {
            _tutorialState.NextStep();
        }
    }
}