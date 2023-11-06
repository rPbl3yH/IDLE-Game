using System.Linq;
using App.Gameplay;
using App.Gameplay.Building;
using App.Gameplay.LevelStorage;
using App.Meta;
using VContainer.Unity;

namespace Modules.Tutorial.Content
{
    public class BuildBarn_TutorialStep : IInitializable
    {
        private const string HINT_TEXT = "Build barn";
        
        private readonly BuildingConstructionService _buildingConstructionService;
        private readonly TutorialState _tutorialState;
        private readonly TutorialViewSystem _tutorialViewSystem;

        public BuildBarn_TutorialStep(
            BuildingConstructionService buildingConstructionService,
            TutorialState tutorialState,
            TutorialViewSystem tutorialViewSystem
            )
        {
            _buildingConstructionService = buildingConstructionService;
            _tutorialState = tutorialState;
            _tutorialViewSystem = tutorialViewSystem;
        }

        public void Initialize()
        {
            _tutorialState.StepStarted += TutorialStateOnStepStarted;
            _tutorialState.StepFinished += TutorialStateOnStepFinished;
        }

        private void TutorialStateOnStepFinished(TutorialStep tutorialStep)
        {
            if (tutorialStep != TutorialStep.BuildBarn)
            {
                return;    
            }
            
            _tutorialViewSystem.Hide();
        }

        private void TutorialStateOnStepStarted(TutorialStep tutorialStep)
        {
            if (tutorialStep != TutorialStep.BuildBarn)
            {
                return;
            }
            
            var services = _buildingConstructionService.GetServices();
            var constructionModel = services.FirstOrDefault(model => model.BuildingModel is BarnModel);

            if (constructionModel != null)
            {
                constructionModel.IsEnable.Value = true;
                constructionModel.Built.AddListener(OnBuilt);
                _tutorialViewSystem.Show(constructionModel.UnloadingPoint, HINT_TEXT);
            }
        }

        private void OnBuilt(BuildingModel buildingModel)
        {
            _tutorialState.NextStep();
        }
    }
}