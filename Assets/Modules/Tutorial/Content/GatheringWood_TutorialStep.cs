using App.Gameplay;
using App.Gameplay.Building;
using App.Gameplay.Character.Scripts.Model;
using App.Gameplay.Player;
using App.Gameplay.Resource;
using App.Meta;
using I2.Loc;
using VContainer.Unity;

namespace Modules.Tutorial.Content
{
    public class GatheringWood_TutorialStep : IInitializable
    {
        private readonly TutorialState _tutorialState;
        private readonly ResourceService _resourceService;
        private readonly BuildingConstructionService _buildingConstructionService;
        private readonly PlayerSpawner _playerSpawner;
        private readonly TutorialViewSystem _tutorialViewSystem;

        private PlayerModel _playerModel;

        public GatheringWood_TutorialStep(
            ResourceService resourceService, 
            BuildingConstructionService buildingConstructionService, 
            PlayerSpawner playerSpawner,
            TutorialState tutorialState,
            TutorialViewSystem tutorialViewSystem
            )
        {
            _resourceService = resourceService;
            _buildingConstructionService = buildingConstructionService;
            _playerSpawner = playerSpawner;
            _tutorialState = tutorialState;
            _tutorialViewSystem = tutorialViewSystem;
        }

        void IInitializable.Initialize()
        {
            _playerSpawner.Spawned += PlayerSpawnerOnSpawned;
            _tutorialState.StepStarted += TutorialStateOnStepStarted;
            _tutorialState.StepFinished += TutorialStateOnStepFinished;
            
            _buildingConstructionService.HideAllConstruction();
            _resourceService.SetActiveResourceType(ResourceType.Stone, false);
        }

        private void TutorialStateOnStepStarted(TutorialStep tutorialStep)
        {
            if (tutorialStep != TutorialStep.GatheringWood)
            {
                return;
            }
            
            _playerModel.CharacterModel.Gathered.AddListener(OnGathered);
            var resource = _resourceService.GetClosetResource(_playerModel.CharacterModel.Root, ResourceType.Wood);
            var text = LocalizationManager.GetTranslation(ScriptTerms.Tutorial.GatheringWood);
            _tutorialViewSystem.Show(resource.transform, text);
        }

        private void TutorialStateOnStepFinished(TutorialStep tutorialStep)
        {
            if (tutorialStep != TutorialStep.GatheringWood)
            {
                return;
            }
            
            _playerModel.CharacterModel.Gathered.RemoveListener(OnGathered);
            _tutorialViewSystem.Hide();
        }

        private void PlayerSpawnerOnSpawned(PlayerModel player)
        {
            _playerSpawner.Spawned -= PlayerSpawnerOnSpawned;
            _playerModel = player;
        }

        private void OnGathered()
        {
            _tutorialState.FinishStep();     
        }
    }
}