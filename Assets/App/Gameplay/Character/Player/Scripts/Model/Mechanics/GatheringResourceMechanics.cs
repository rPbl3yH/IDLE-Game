using System;
using App.Gameplay.Resource;
using Atomic;
using UnityEngine;

namespace App.Gameplay
{
    public class GatheringResourceMechanics
    {
        private readonly AtomicEvent _gathered;
        private readonly AtomicVariable<int> _gatheringCount;
        private readonly AtomicVariable<ResourceModel> _targetResource;
        private readonly AtomicVariable<int> _amount;
        private readonly AtomicVariable<int> _maxAmount;

        public GatheringResourceMechanics(
            AtomicVariable<ResourceModel> targetResource,
            AtomicVariable<int> gatheringCount,
            AtomicVariable<int> amount,
            AtomicVariable<int> maxAmount,
            AtomicEvent gathered)
        {
            _targetResource = targetResource;
            _gatheringCount = gatheringCount;
            _gathered = gathered;
            _amount = amount;
            _maxAmount = maxAmount;
        }

        public void OnEnable()
        {
            _gathered.AddListener(OnGathered);
        }

        public void OnDisable()
        {
            _gathered.RemoveListener(OnGathered);
        }

        private void OnGathered()
        {
            var amount = _targetResource.Value.Amount.Value;
            
            if (amount == 0)
            {
                return;
            }

            var gatheringCount = Math.Min(amount, _gatheringCount.Value);
            var availableAmount = _maxAmount.Value - _amount.Value;
            gatheringCount = Math.Min(gatheringCount, availableAmount);
            var type = _targetResource.Value.ResourceType;

            _targetResource.Value.Gathered?.Invoke(gatheringCount);

            _amount.Value += gatheringCount;
            //Debug.Log($"Gathered {type} {gatheringCount} in player");
        }
    }
}