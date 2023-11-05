using App.Gameplay;
using App.Gameplay.Building;
using App.Gameplay.Character.Scripts.Model;
using App.Gameplay.Player;
using App.Gameplay.Resource;
using VContainer.Unity;

namespace Modules.Tutorial.Content
{
    public class GatheringWood_TutorialStep : IInitializable, IPostStartable
    {
        private readonly TutorialState _tutorialState;
        private readonly ResourceService _resourceService;
        private readonly BuildingConstructionService _buildingConstructionService;
        private readonly PlayerSpawner _playerSpawner;

        private PlayerModel _playerModel;

        public GatheringWood_TutorialStep(
            ResourceService resourceService, 
            BuildingConstructionService buildingConstructionService, 
            PlayerSpawner playerSpawner,
            TutorialState tutorialState
            )
        {
            _resourceService = resourceService;
            _buildingConstructionService = buildingConstructionService;
            _playerSpawner = playerSpawner;
            _tutorialState = tutorialState;
        }

        void IInitializable.Initialize()
        {
            _playerSpawner.Spawned += PlayerSpawnerOnSpawned;
            _tutorialState.StepStarted += TutorialStateOnStepStarted;
        }

        void IPostStartable.PostStart()
        {
            _buildingConstructionService.HideAllConstruction();
            _resourceService.SetActiveResourceType(ResourceType.Stone, false);
        }

        private void TutorialStateOnStepStarted(TutorialStep tutorialStep)
        {
            if (tutorialStep == TutorialStep.GatheringWood)
            {
                _playerModel.CharacterModel.Gathered.AddListener(OnGathered);
            }
        }

        private void PlayerSpawnerOnSpawned(PlayerModel player)
        {
            _playerModel = player;
        }

        private void OnGathered()
        {
            _playerSpawner.Spawned -= PlayerSpawnerOnSpawned;
            _playerModel.CharacterModel.Gathered.RemoveListener(OnGathered);
            _tutorialState.NextStep();            
        }
    }
}