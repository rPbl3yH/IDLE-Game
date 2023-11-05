using System;
using UnityEngine;

namespace Modules.Tutorial
{
    public class TutorialState
    {
        public event Action<TutorialStep> StepStarted;
        public event Action<TutorialStep> StopFinished;
        public event Action Completed;
    
        public bool IsCompleted { get; private set; }
        public TutorialStep CurrentStep { get; private set; } = TutorialStep.Start;

        public void NextStep()
        {
            CurrentStep++;
        
            if (CurrentStep == TutorialStep.End)
            {
                IsCompleted = true;
                Completed?.Invoke();
            }
            else
            {
                Debug.Log("Step started " + CurrentStep);
                StepStarted?.Invoke(CurrentStep);
            }
        }

        public void FinishStep(bool moveNext = true)
        {
            StopFinished?.Invoke(CurrentStep);
        
            if (moveNext)
            {
                NextStep();
            }
        }
    }
}