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
        private readonly ResourceStorage _resourceStorage;

        public GatheringResourceMechanics(
            ResourceStorage resourceStorage,
            AtomicVariable<ResourceModel> targetResource,
            AtomicVariable<int> gatheringCount,
            AtomicEvent gathered)
        {
            _resourceStorage = resourceStorage;
            _targetResource = targetResource;
            _gatheringCount = gatheringCount;
            _gathered = gathered;
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
            var type = _targetResource.Value.ResourceType;

            _targetResource.Value.Gathered?.Invoke(gatheringCount);
            
            _resourceStorage.Add(type, gatheringCount);
            Debug.Log($"Gathered {type} {gatheringCount} in player");
        }
    }
}