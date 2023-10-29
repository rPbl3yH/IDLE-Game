using System;
using App.Gameplay.Resource;
using App.Gameplay.Resource.Model;
using Modules.Atomic.Actions;
using Modules.Atomic.Values;
using Sirenix.OdinInspector;
using UnityEngine;

namespace App.Gameplay.Character.Scripts.Model.Actions
{
    [Serializable]
    public class DetectionResourceFunction : IAtomicFunction<ResourceModel>
    {
        private readonly ResourceService _resourceService;
        private readonly IAtomicVariable<ResourceType> _resourceType;
        private readonly IAtomicValue<int> _amount;
        private readonly Transform _root;

        public DetectionResourceFunction(
            CharacterModel characterModel,
            ResourceService resourceService)
        {
            _resourceType = characterModel.ResourceType;
            _amount = characterModel.ResourceAmount;
            _root = characterModel.Root;
            _resourceService = resourceService;
        }

        [Button]
        public ResourceModel GetResult()
        {
            ResourceModel resource;
            
            if (_amount.Value == 0)
            {
                resource = _resourceService.GetClosetResource(_root);
                if (resource != null)
                {
                    _resourceType.Value = resource.ResourceType;
                }
            }
            else
            {
                resource = _resourceService.GetClosetResource(_root, _resourceType.Value);
            }

            return resource;
        }
    }
}