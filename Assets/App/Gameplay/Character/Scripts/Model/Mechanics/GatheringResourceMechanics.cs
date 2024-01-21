using System;
using App.Gameplay.Resource.Model;
using Atomic.Elements;

namespace App.Gameplay.Character.Scripts.Model.Mechanics
{
    public class GatheringResourceMechanics
    {
        private readonly AtomicEvent _gathered;
        private readonly IAtomicValue<int> _gatheringCount;
        private readonly IAtomicVariable<ResourceModel> _targetResource;
        private readonly IAtomicVariable<int> _amount;
        private readonly IAtomicValue<int> _maxAmount;

        private readonly IAtomicVariable<ResourceType> _resourceType;

        public GatheringResourceMechanics(
            IAtomicVariable<ResourceModel> targetResource,
            IAtomicVariable<ResourceType> resourceType,
            IAtomicValue<int> gatheringCount,
            IAtomicVariable<int> amount,
            IAtomicValue<int> maxAmount,
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
            _gathered.Subscribe(OnGathered);
        }

        public void OnDisable()
        {
            _gathered.Unsubscribe(OnGathered);
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
        }
    }
}