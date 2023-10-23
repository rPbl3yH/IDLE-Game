﻿using App.Gameplay.AI.Model;
using App.Gameplay.Resource;
using Atomic;
using UnityEngine;

namespace App.Gameplay.AI.States
{
    public class GatheringResourceState : StateMachine
    {
        private readonly GatheringResourceData _gatheringData;
        private readonly MoveToPositionData _moveData;
        
        private readonly IState _moveState;
        private readonly IState _detectionState;
        private readonly AtomicVariable<Vector3> _moveDirection;
        private readonly AtomicVariable<ResourceModel> _targetResource;
        private readonly AtomicVariable<bool> _canGathering;
        private readonly AtomicVariable<float> _gatheringDistance;
        private readonly Transform _root;

        public GatheringResourceState(
            GatheringResourceData gatheringData, 
            MoveToPositionData moveData, 
            IState moveState,
            IState detectionState,
            CharacterModel characterModel)
        {
            _gatheringData = gatheringData;
            _moveData = moveData;
            _moveState = moveState;
            _detectionState = detectionState;
            _moveDirection = characterModel.MoveDirection;
            _targetResource = characterModel.TargetResource;
            _canGathering = characterModel.CanGathering;
            _gatheringDistance = characterModel.GatheringDistance;
            _root = characterModel.Root;
        }

        public GatheringResourceState(
            GatheringResourceData gatheringData, 
            MoveToPositionData moveData, 
            IState moveState,
            IState detectionState,
            AtomicVariable<Vector3> moveDirection, 
            AtomicVariable<ResourceModel> targetResource,
            AtomicVariable<bool> canGathering,
            AtomicVariable<float> gatheringDistance)
        {
            _gatheringData = gatheringData;
            _moveData = moveData;
            _moveState = moveState;
            _detectionState = detectionState;
            _moveDirection = moveDirection;
            _targetResource = targetResource;
            _canGathering = canGathering;
            _gatheringDistance = gatheringDistance;
        }

        public override void Enter()
        {
            base.Enter();
            _gatheringData.IsEnable = true;
            SwitchState(_detectionState);
        }

        public override void Exit()
        {
            _gatheringData.IsEnable = false;
            ClearTargetResource();
            base.Exit();
        }

        public override void Update(float deltaTime)
        {
            if (!_gatheringData.IsEnable)
            {
                return;
            }
            
            base.Update(deltaTime);
            
            if (_targetResource.Value == null || _targetResource.Value.Amount.Value == 0)
            {
                SwitchState(_detectionState);
                return;
            }
            
            if (!_moveData.IsPositionReached)
            {
                SwitchState(_moveState);
                return;
            }
            
            if (_moveDirection.Value != Vector3.zero)
            {
                return;
            }
            
            SwitchState(null);

            var delta = _targetResource.Value.transform.position - _root.position;
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