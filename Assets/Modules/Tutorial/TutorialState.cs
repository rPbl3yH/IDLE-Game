using System;
using UnityEngine;

namespace Modules.Tutorial
{
    public class TutorialState
    {
        public event Action<TutorialStep> StepStarted;
        public event Action<TutorialStep> StepFinished;
        public event Action Completed;
    
        public bool IsCompleted { get; set; }
        public TutorialStep CurrentStep { get; set; } = TutorialStep.Start;

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
            StepFinished?.Invoke(CurrentStep);
        
            if (moveNext)
            {
                NextStep();
            }
        }
    }
}