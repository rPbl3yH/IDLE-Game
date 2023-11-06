using App.Gameplay;
using App.Gameplay.Building;
using App.Gameplay.Character.Scripts.Model;
using App.Gameplay.Player;
using App.Gameplay.Resource;
using App.Meta;
using App.Meta.HintTextService;
using VContainer.Unity;

namespace Modules.Tutorial.Content
{
    public class GatheringWood_TutorialStep : IInitializable
    {
        private const string HINT_TEXT = "Добудь дерево";
        
        private readonly TutorialState _tutorialState;
        private readonly ResourceService _resourceService;
        private readonly BuildingConstructionService _buildingConstructionService;
        private readonly PlayerSpawner _playerSpawner;
        private readonly PlayerPointerController _playerPointerController;
        private readonly ObjectPointerController _objectPointerController;
        private readonly HintTextObserver _hintTextObserver;


        private PlayerModel _playerModel;

        public GatheringWood_TutorialStep(
            ResourceService resourceService, 
            BuildingConstructionService buildingConstructionService, 
            PlayerSpawner playerSpawner,
            TutorialState tutorialState,
            PlayerPointerController playerPointerController,
            ObjectPointerController objectPointerController,
            HintTextObserver hintTextObserver
            )
        {
            _resourceService = resourceService;
            _buildingConstructionService = buildingConstructionService;
            _playerSpawner = playerSpawner;
            _tutorialState = tutorialState;
            _playerPointerController = playerPointerController;
            _objectPointerController = objectPointerController;
            _hintTextObserver = hintTextObserver;
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
            _playerPointerController.SetTarget(resource.transform);
            _objectPointerController.SetTarget(resource.transform);
            _hintTextObserver.Show(HINT_TEXT);
        }

        private void TutorialStateOnStepFinished(TutorialStep tutorialStep)
        {
            if (tutorialStep != TutorialStep.GatheringWood)
            {
                return;
            }
            
            _playerModel.CharacterModel.Gathered.RemoveListener(OnGathered);
            _playerPointerController.SetTarget(null);  
            _objectPointerController.SetTarget(null);
            _hintTextObserver.Hide();
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