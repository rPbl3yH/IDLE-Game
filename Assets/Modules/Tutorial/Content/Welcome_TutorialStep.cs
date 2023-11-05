using App.GameEngine.Input.Handlers;
using App.UI.UIManager;
using SimpleInputNamespace;
using UnityEngine;
using VContainer.Unity;

namespace Modules.Tutorial.Content
{
    public class Welcome_TutorialStep : IInitializable
    {
        private readonly TutorialState _tutorialState;
        private readonly BaseUIView _panel;
        private readonly IInputHandler _inputHandler;

        public Welcome_TutorialStep(TutorialState tutorialState, UIPanelManager uiPanelManager, IInputHandler inputHandler)
        {
            _tutorialState = tutorialState;
            _inputHandler = inputHandler;
            _panel = uiPanelManager.GetPanel(UIPanelType.Welcome);
            _panel.Hide();    
        }

        void IInitializable.Initialize()
        {
            _tutorialState.StepStarted += TutorialStateOnStepStarted;
        }

        private void TutorialStateOnStepStarted(TutorialStep tutorialStep)
        {
            _tutorialState.StepStarted -= TutorialStateOnStepStarted;
            
            if (tutorialStep == TutorialStep.Welcome)
            {
                _inputHandler.Disable();
                _panel.Show();
                _panel.Hidden += PanelOnHidden;
            }
        }

        private void PanelOnHidden()
        {
            _panel.Hidden -= PanelOnHidden;
            _inputHandler.Enable();
            _tutorialState.NextStep();
        }
    }
}