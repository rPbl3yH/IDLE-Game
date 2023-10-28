using System;
using App.Gameplay.Resource;
using App.Gameplay.Resource.Model;
using App.Gameplay.ResourceStorage;
using Modules.Atomic.Actions;
using Modules.Atomic.Values;
using Sirenix.OdinInspector;
using UnityEngine;

namespace App.Gameplay.Character.Scripts.Model.Actions
{
    [Serializable]
    public class DetectionResourceAction : IAtomicAction
    {
        private readonly ResourceService _resourceService;
        private readonly IAtomicVariable<ResourceModel> _targetResource;
        private readonly IAtomicVariable<ResourceType> _resourceType;
        private readonly IAtomicValue<int> _amount;
        private readonly Transform _root;

        public DetectionResourceAction(
            CharacterModel characterModel,
            ResourceService resourceService)
        {
            _targetResource = characterModel.TargetResource;
            _resourceType = characterModel.ResourceType;
            _amount = characterModel.Amount;
            _root = characterModel.Root;
            _resourceService = resourceService;
        }

        [Button]
        public void Invoke()
        {
            ResourceModel resource;
            
            if (_amount.Value == 0)
            {
                resource = _resourceService.GetClosetResource(_root);
                _resourceType.Value = resource.ResourceType;
            }
            else
            {
                resource = _resourceService.GetClosetResource(_root, _resourceType.Value);
            }

            _targetResource.Value = resource;
        }
    }
}