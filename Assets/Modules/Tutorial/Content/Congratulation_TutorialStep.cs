using App.UI.UIManager;
using VContainer.Unity;

namespace Modules.Tutorial.Content
{
    public class Congratulation_TutorialStep : IInitializable
    {
        private readonly TutorialState _tutorialState;
        private readonly BaseUIView _endPanel;

        public Congratulation_TutorialStep(TutorialState tutorialState, UIPanelManager uiPanelManager)
        {
            _tutorialState = tutorialState;
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
            _endPanel.Hidden -= EndPanelOnHidden;
            _endPanel.Hide();
        }

        private void TutorialStateOnStepStarted(TutorialStep tutorialStep)
        {
            if (tutorialStep != TutorialStep.Congratulation)
            {
                return;
            }
            
            _endPanel.Show();
            _endPanel.Hidden += EndPanelOnHidden;
        }

        private void EndPanelOnHidden()
        {
            _tutorialState.FinishStep();
        }
    }
}