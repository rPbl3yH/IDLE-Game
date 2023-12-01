using App.GameEngine.Input.Handlers;
using App.Gameplay.Building;
using App.UI.UIManager;
using VContainer.Unity;

namespace Modules.Tutorial.Content
{
    public class Congratulation_TutorialStep : IInitializable
    {
        private readonly TutorialState _tutorialState;
        private readonly BaseUIView _endPanel;
        private readonly BuildingConstructionService _constructionService;
        private readonly IInputHandler _inputHandler;

        public Congratulation_TutorialStep(
            TutorialState tutorialState, 
            UIPanelManager uiPanelManager,
            BuildingConstructionService constructionService,
            IInputHandler inputHandler
            )
        {
            _tutorialState = tutorialState;
            _constructionService = constructionService;
            _inputHandler = inputHandler;
            _endPanel = uiPanelManager.GetPanel(UIPanelType.TutorialEnd);
            _endPanel.Hide();
        }

        public void Initialize()
        {
            _tutorialState.StepStarted += TutorialStateOnStepStarted;
            _tutorialState.StepFinished += TutorialStateOnStepFinished;
        }

        private void TutorialStateOnStepFinished(TutorialStep tutorialStep)
        {
            if (tutorialStep != TutorialStep.Congratulation)
            {
                return;
            }

            _inputHandler.Enable();
            
            _constructionService.ShowAll();
            _endPanel.Hidden -= EndPanelOnHidden;
            _endPanel.Hide();
        }

        private void TutorialStateOnStepStarted(TutorialStep tutorialStep)
        {
            if (tutorialStep != TutorialStep.Congratulation)
            {
                return;
            }
            
            _inputHandler.Disable();
            
            _endPanel.Show();
            _endPanel.Hidden += EndPanelOnHidden;
        }

        private void EndPanelOnHidden()
        {
            _tutorialState.FinishStep();
        }
    }
}