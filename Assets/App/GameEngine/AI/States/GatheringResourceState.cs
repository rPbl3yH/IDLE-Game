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
        private readonly IAtomicAction _detectionAction;
        private readonly AtomicVariable<Vector3> _moveDirection;
        private readonly AtomicVariable<ResourceModel> _targetResource;
        private readonly AtomicVariable<bool> _canGathering;
        private readonly AtomicVariable<float> _gatheringDistance;
        private readonly AtomicVariable<float> _stoppingDistance;
        private readonly Transform _root;

        public GatheringResourceState(
            GatheringResourceData gatheringData, 
            MoveToPositionData moveData, 
            IState moveState,
            CharacterModel characterModel)
        {
            _gatheringData = gatheringData;
            _moveData = moveData;
            _moveState = moveState;
            _detectionAction = characterModel.DetectionResourceAction;
            _moveDirection = characterModel.MoveDirection;
            _targetResource = characterModel.TargetResource;
            _canGathering = characterModel.CanGathering;
            _gatheringDistance = characterModel.GatheringDistance;
            _root = characterModel.Root;
            _stoppingDistance = characterModel.GatheringDistance;
        }

        public GatheringResourceState(
            GatheringResourceData gatheringData, 
            MoveToPositionData moveData, 
            IState moveState,
            IAtomicAction detectionAction,
            AtomicVariable<Vector3> moveDirection, 
            AtomicVariable<ResourceModel> targetResource,
            AtomicVariable<bool> canGathering,
            AtomicVariable<float> gatheringDistance)
        {
            _gatheringData = gatheringData;
            _moveData = moveData;
            _moveState = moveState;
            _detectionAction = detectionAction;
            _moveDirection = moveDirection;
            _targetResource = targetResource;
            _canGathering = canGathering;
            _gatheringDistance = gatheringDistance;
        }

        public override void Enter()
        {
            base.Enter();
            _moveData.StoppingDistance = _stoppingDistance.Value;
            _gatheringData.IsEnable = true;
            FindResource();
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
                FindResource();
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

        private void FindResource()
        {
            _detectionAction?.Invoke();
            
            if (_targetResource.Value != null)
            {
                _moveData.TargetPosition = _targetResource.Value.transform.position;
                _moveData.IsPositionReached = false;
            }
        }

        private void ClearTargetResource()
        {
            _canGathering.Value = false;
        }
    }
}