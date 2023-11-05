using UnityEngine;
using VContainer.Unity;

namespace Modules.Tutorial.Content
{
    public class WelcomeTutorialStep : IInitializable
    {
        private readonly TutorialState _tutorialState;

        public WelcomeTutorialStep(TutorialState tutorialState)
        {
            _tutorialState = tutorialState;
        }

        private void TutorialStateOnStepStarted(TutorialStep tutorialStep)
        {
            Debug.Log("step start = " + tutorialStep);
            if (tutorialStep == TutorialStep.Welcome)
            {
                Debug.Log("Добро пожаловать в игру");
            }
        }

        void IInitializable.Initialize()
        {
            _tutorialState.StepStarted += TutorialStateOnStepStarted;
        }
    }
}