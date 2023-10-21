using System;
using Atomic;
using UnityEngine;

namespace App.Gameplay.Resource
{
    public class GatheringMechanics
    {
        private readonly AtomicEvent<int> _gatheringRequested;
        private readonly AtomicEvent<int> _gatheringSuccess;
        private readonly AtomicVariable<int> _amount;

        public GatheringMechanics(AtomicEvent<int> gatheringRequested, AtomicEvent<int> gatheringSuccess, AtomicVariable<int> amount)
        {
            _gatheringRequested = gatheringRequested;
            _gatheringSuccess = gatheringSuccess;
            _amount = amount;
        }

        public void OnEnable()
        {
            _gatheringRequested.AddListener(OnRequestGathered);
            _gatheringSuccess.AddListener(OnGathered);
        }

        public void OnDisable()
        {
            _gatheringSuccess.RemoveListener(OnGathered);
            _gatheringRequested.RemoveListener(OnRequestGathered);
        }
        
        private void OnGathered(int count)
        {
            _amount.Value -= count;
            Debug.Log($"Gathered {count}");
        }

        private void OnRequestGathered(int value)
        {
            var gatheredCount = Math.Min(value, _amount.Value);
            
            if (gatheredCount > 0)
            {
                _gatheringSuccess?.Invoke(gatheredCount);
            }
        }
    }
}