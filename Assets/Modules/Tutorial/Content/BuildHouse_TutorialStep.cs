using System.Linq;
using App.Gameplay;
using App.Gameplay.Building;
using App.Gameplay.Building.House;
using App.Gameplay.Resource;
using App.Meta;
using I2.Loc;
using VContainer.Unity;

namespace Modules.Tutorial.Content
{
    public class BuildHouse_TutorialStep : IInitializable
    {
        private readonly TutorialState _tutorialState;
        private readonly ResourceService _resourceService;
        private readonly BuildingConstructionService _buildingConstructionService;
        private readonly TutorialViewSystem _tutorialViewSystem;

        public BuildHouse_TutorialStep(
            TutorialState tutorialState, 
            ResourceService resourceService, 
            BuildingConstructionService buildingConstructionService,
            TutorialViewSystem tutorialViewSystem
            )
        {
            _tutorialState = tutorialState;
            _resourceService = resourceService;
            _buildingConstructionService = buildingConstructionService;
            _tutorialViewSystem = tutorialViewSystem;
        }

        void IInitializable.Initialize()
        {
            _tutorialState.StepStarted += TutorialStateOnStepStarted;
            _tutorialState.StepFinished += TutorialStateOnStepFinished;
        }

        private void TutorialStateOnStepFinished(TutorialStep tutorialStep)
        {
            if (tutorialStep != TutorialStep.BuildHouse)
            {
                return;
            }
            
            _tutorialViewSystem.Hide();
        }

        private void TutorialStateOnStepStarted(TutorialStep tutorialStep)
        {
            if (tutorialStep != TutorialStep.BuildHouse)
            {
                return;
            }
            
            _resourceService.SetActiveResourceType(ResourceType.Stone, true);
            
            var constructionModel = _buildingConstructionService.GetServices()
                .FirstOrDefault(model => model.BuildingModel is HouseBuildingModel);
                
            if (constructionModel != null)
            {
                constructionModel.IsEnable.Value = true;
                constructionModel.Built.Subscribe(OnBuilt);
                var text = LocalizationManager.GetTranslation(ScriptTerms.Tutorial.BuildHouse);
                _tutorialViewSystem.Show(constructionModel.UnloadingPoint, text);
            }
        }

        private void OnBuilt(BuildingModel buildingModel)
        {
            _tutorialState.FinishStep();
        }
    }
}