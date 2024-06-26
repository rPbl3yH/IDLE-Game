using System;

namespace Modules.Elementary.Time
{
    public interface IPeriod
    {
        event Action OnStarted;

        event Action OnPeriodEvent;

        event Action OnStoped;
        
        float Duration { get; set; }

        bool IsActive { get; }

        void Play();

        void Stop();
    }
}