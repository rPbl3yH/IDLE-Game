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
        public AtomicVariable<ResourceModel> TargetResource;
        public AtomicVariable<bool> CanGathering;
        public ResourceService ResourceService;
        public Transform Root;
    }
    
    public class DetectionResourceState : StateMachine
    {
        private readonly DetectionResourceData _resourceData;
        private readonly MoveToPositionData _moveData;

        private readonly IState _moveState;

        public DetectionResourceState(
            DetectionResourceData resourceData, MoveToPositionData moveData, IState moveState)
        {
            _resourceData = resourceData;
            _moveData = moveData;
            _moveState = moveState;
        }
        
        public override void Exit()
        {
            ClearTargetResource();
            base.Exit();
        }
        
        public override void Update(float deltaTime)
        {
            if (!_resourceData.IsEnable)
            {
                return;
            }
            
            SetupClosetResources();
            
            if (_resourceData.TargetResource.Value == null)
            {
                return;
            }

            _moveData.TargetPosition = _resourceData.TargetResource.Value.transform.position;
            
            if (!_moveData.IsPositionReached || _moveData.IsEnable)
            {
                SwitchState(_moveState);
            }
            else
            {
                _resourceData.CanGathering.Value = true;
            }
            
            base.Update(deltaTime);
        }

        private void SetupClosetResources()
        {
            var closetResource = _resourceData.ResourceService.GetClosetResources(_resourceData.Root);

            if (closetResource.Amount.Value == 0)
            {
                ClearTargetResource();
                return;
            }

            _resourceData.TargetResource.Value = closetResource;
        }

        private void ClearTargetResource()
        {
            _resourceData.CanGathering.Value = false;
            _resourceData.TargetResource.Value = null;
        }
    }
}