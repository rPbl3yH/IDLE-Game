using System.Linq;
using App.Gameplay;
using App.Gameplay.Building;
using App.Gameplay.Building.House;
using App.Gameplay.Resource;
using UnityEngine;
using VContainer.Unity;

namespace Modules.Tutorial.Content
{
    public class BuildHouse_TutorialStep : IInitializable
    {
        private readonly TutorialState _tutorialState;
        private readonly ResourceService _resourceService;
        private readonly BuildingConstructionService _buildingConstructionService;

        public BuildHouse_TutorialStep(
            TutorialState tutorialState, 
            ResourceService resourceService, 
            BuildingConstructionService buildingConstructionService)
        {
            _tutorialState = tutorialState;
            _resourceService = resourceService;
            _buildingConstructionService = buildingConstructionService;
        }

        public void Initialize()
        {
            _tutorialState.StepStarted += TutorialStateOnStepStarted;
        }

        private void TutorialStateOnStepStarted(TutorialStep tutorialStep)
        {
            if (tutorialStep != TutorialStep.BuildHouse)
            {
                return;
            }
            
            _resourceService.SetActiveResourceType(ResourceType.Stone, true);
            
            var buildingConstructionModel = _buildingConstructionService.GetServices()
                .FirstOrDefault(model => model.BuildingModel is HouseBuildingModel);
                
            if (buildingConstructionModel != null)
            {
                buildingConstructionModel.IsEnable.Value = true;
                buildingConstructionModel.Built.AddListener(OnBuilt);
            }
        }

        private void OnBuilt(BuildingModel buildingModel)
        {
            _tutorialState.NextStep();
        }
    }
}