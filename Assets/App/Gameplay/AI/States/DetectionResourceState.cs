using System;
using App.Gameplay.AI.Model;
using App.Gameplay.Resource;
using Atomic;
using UnityEngine;

namespace App.Gameplay.AI.States
{
    [Serializable]
    public class DetectionResourceData
    {
        public bool IsEnable;
        public ResourceService ResourceService;
        public Transform Root;
    }
    
    public class DetectionResourceState : StateMachine
    {
        private readonly DetectionResourceData _resourceData;
        private readonly MoveToPositionData _moveData;
        
        private readonly IState _moveState;
        private readonly AtomicVariable<Vector3> _moveDirection;
        private readonly AtomicVariable<ResourceModel> _targetResource;
        private readonly AtomicVariable<bool> _canGathering;
        private readonly AtomicVariable<float> _gatheringDistance;

        public DetectionResourceState(
            DetectionResourceData resourceData, 
            MoveToPositionData moveData, 
            IState moveState,
            CharacterModel characterModel)
        {
            _resourceData = resourceData;
            _moveData = moveData;
            _moveState = moveState;
            _moveDirection = characterModel.MoveDirection;
            _targetResource = characterModel.TargetResource;
            _canGathering = characterModel.CanGathering;
            _gatheringDistance = characterModel.GatheringDistance;
        }
        
        public DetectionResourceState(
            DetectionResourceData resourceData, 
            MoveToPositionData moveData, 
            IState moveState,
            AtomicVariable<Vector3> moveDirection, 
            AtomicVariable<ResourceModel> targetResource,
            AtomicVariable<bool> canGathering,
            AtomicVariable<float> gatheringDistance)
        {
            _resourceData = resourceData;
            _moveData = moveData;
            _moveState = moveState;
            _moveDirection = moveDirection;
            _targetResource = targetResource;
            _canGathering = canGathering;
            _gatheringDistance = gatheringDistance;
        }
        
        public override void Exit()
        {
            ClearTargetResource();
            base.Exit();
        }
        
        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            
            if (_moveDirection.Value != Vector3.zero)
            {
                return;
            }

            _targetResource.Value = _resourceData.ResourceService.GetClosetResources(_resourceData.Root);

            if (_targetResource.Value == null)
            {
                return;
            }

            _moveData.TargetPosition = _targetResource.Value.transform.position;

            if (!_moveData.IsPositionReached)
            {
                SwitchState(_moveState);
                return;
            }

            var delta = _targetResource.Value.transform.position - _resourceData.Root.transform.position;
            var distance = delta.magnitude;

            if (distance <= _gatheringDistance.Value)
            {
                _canGathering.Value = true;
            }
            else
            {
                _canGathering.Value = false;
            }
        }

        private void ClearTargetResource()
        {
            _canGathering.Value = false;
        }
    }
}