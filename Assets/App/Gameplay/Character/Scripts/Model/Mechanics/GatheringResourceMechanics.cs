using System;
using App.Gameplay.Resource.Model;
using Modules.Atomic.Actions;
using Modules.Atomic.Values;

namespace App.Gameplay.Character.Scripts.Model.Mechanics
{
    public class GatheringResourceMechanics
    {
        private readonly AtomicEvent _gathered;
        private readonly AtomicVariable<int> _gatheringCount;
        private readonly AtomicVariable<ResourceModel> _targetResource;
        private readonly AtomicVariable<int> _amount;
        private readonly AtomicVariable<int> _maxAmount;

        private readonly AtomicVariable<ResourceType> _resourceType;

        public GatheringResourceMechanics(
            AtomicVariable<ResourceModel> targetResource,
            AtomicVariable<ResourceType> resourceType,
            AtomicVariable<int> gatheringCount,
            AtomicVariable<int> amount,
            AtomicVariable<int> maxAmount,
            AtomicEvent gathered)
        {
            _targetResource = targetResource;
            _resourceType = resourceType;
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

            var targetResourceType = _targetResource.Value.ResourceType;
            
            if (_resourceType.Value != targetResourceType)
            {
                _resourceType.Value = targetResourceType;
            }
            
            _targetResource.Value.Gathered?.Invoke(gatheringCount);

            _amount.Value += gatheringCount;
            //Debug.Log($"Gathered {type} {gatheringCount} in player");
        }
    }
}