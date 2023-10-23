using App.Gameplay.AI.Model;
using App.Gameplay.Resource;
using Atomic;
using UnityEngine;

namespace App.Gameplay.AI.States
{
    public class DetectionResourceState : IState
    {
        private readonly DetectionResourceData _detectionData;
        private readonly MoveToPositionData _moveData;
        private readonly AtomicVariable<ResourceModel> _targetResource;
        private readonly Transform _root;

        public DetectionResourceState(DetectionResourceData detectionData, MoveToPositionData moveData, AtomicVariable<ResourceModel> targetResource, Transform root)
        {
            _detectionData = detectionData;
            _moveData = moveData;
            _targetResource = targetResource;
            _root = root;
        }

        public void Enter()
        {
            var resource = _detectionData.ResourceService.GetClosetResource(_root);
            _targetResource.Value = resource;
            if (_targetResource.Value == null)
            {
                _moveData.IsPositionReached = true;
            }
            else
            {
                _moveData.TargetPosition = _targetResource.Value.transform.position;
                _moveData.IsPositionReached = false;
            }
        }

        public void Update(float deltaTime)
        {
            
        }

        public void Exit()
        {
        }
    }
}