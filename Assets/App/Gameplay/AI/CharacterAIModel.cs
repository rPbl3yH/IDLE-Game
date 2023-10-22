using App.Gameplay.AI.Model;
using App.Gameplay.AI.States;
using UnityEngine;

namespace App.Gameplay.AI
{
    public class CharacterAIModel : MonoBehaviour
    {
        public MoveToPositionData MoveToPositionData;
        public DetectionResourceData DetectionResourceData;
        public UnloadResourceData UnloadResourceData;

        [SerializeField] private CharacterModel _characterModel;

        private MoveToPositionState _moveToPositionState;
        private DetectionResourceState _detectionResourceState;
        private UnloadingResourceState _unloadingResourceState;
        private StateMachine _stateMachine;
        
        private void Awake()
        {
            _stateMachine = new StateMachine();
            
            _moveToPositionState =
                new MoveToPositionState(MoveToPositionData, _characterModel.MoveDirection, _characterModel.Root);

            _detectionResourceState =
                new DetectionResourceState(DetectionResourceData, MoveToPositionData, _moveToPositionState, _characterModel);

            _unloadingResourceState = new UnloadingResourceState(UnloadResourceData, MoveToPositionData, _characterModel, _moveToPositionState);
            
            _stateMachine.SwitchState(_detectionResourceState);
            
        }

        private void Update()
        {
            if (_characterModel.Amount.Value == 0)
            {
                _stateMachine.SwitchState(_detectionResourceState);
            }
            else
            {
                if (!_characterModel.IsFreeSpace.Value)
                {
                    _stateMachine.SwitchState(_unloadingResourceState);
                }
            }
            
            _stateMachine.Update(Time.deltaTime);
        }
    }
}