using System;
using App.Gameplay.Resource;
using Atomic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace App.Gameplay.AI.States
{
    [Serializable]
    public class DetectionResourceAction : IAtomicAction
    {
        private readonly ResourceService _resourceService;
        private readonly IAtomicVariable<ResourceModel> _targetResource;
        private readonly Transform _root;

        public DetectionResourceAction(
            IAtomicVariable<ResourceModel> targetResource,
            Transform root,
            ResourceService resourceService)
        {
            _targetResource = targetResource;
            _root = root;
            _resourceService = resourceService;
        }

        [Button]
        public void Invoke()
        {
            var resource = _resourceService.GetClosetResource(_root);
            _targetResource.Value = resource;
        }
    }
}