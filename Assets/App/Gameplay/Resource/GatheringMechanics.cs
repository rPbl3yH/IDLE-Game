using System;
using Atomic;
using UnityEngine;

namespace App.Gameplay.Resource
{
    public class GatheringMechanics
    {
        private readonly AtomicEvent<int> _gatheringSuccess;
        private readonly AtomicVariable<int> _amount;

        public GatheringMechanics(AtomicEvent<int> gatheringSuccess, AtomicVariable<int> amount)
        {
            _gatheringSuccess = gatheringSuccess;
            _amount = amount;
        }

        public void OnEnable()
        {
            _gatheringSuccess.AddListener(OnGathered);
        }

        public void OnDisable()
        {
            _gatheringSuccess.RemoveListener(OnGathered);
        }
        
        private void OnGathered(int count)
        {
            _amount.Value -= count;
            Debug.Log($"Gathered {count}");
        }
    }
}