using App.UI.UIManager;
using UnityEngine;
using VContainer.Unity;

namespace Modules.Tutorial.Content
{
    public class WelcomeTutorialStep : IInitializable
    {
        private readonly TutorialState _tutorialState;
        private readonly BaseUIView _panel;
        
        public WelcomeTutorialStep(TutorialState tutorialState, UIPanelManager uiPanelManager)
        {
            _tutorialState = tutorialState;
            _panel = uiPanelManager.GetPanel(UIPanelType.Welcome);
            _panel.Hide();    
        }

        private void TutorialStateOnStepStarted(TutorialStep tutorialStep)
        {
            _tutorialState.StepStarted -= TutorialStateOnStepStarted;
            
            if (tutorialStep == TutorialStep.Welcome)
            {
                _panel.Show();
                _panel.Hidden += PanelOnHidden;
            }
        }

        private void PanelOnHidden()
        {
            _panel.Hidden -= PanelOnHidden;
            _tutorialState.NextStep();
        }

        void IInitializable.Initialize()
        {
            _tutorialState.StepStarted += TutorialStateOnStepStarted;
        }
    }
}