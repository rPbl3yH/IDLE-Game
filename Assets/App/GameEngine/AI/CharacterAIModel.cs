using App.Gameplay.AI.Model;
using App.Gameplay.AI.States;
using UnityEngine;

namespace App.Gameplay.AI
{
    public class CharacterAIModel : MonoBehaviour
    {
        public MoveToPositionData MoveToPositionData;
        public GatheringResourceData GatheringResourceData;
        public UnloadResourceData UnloadResourceData;

        [SerializeField] private CharacterModel _characterModel;

        private DetectionResourceAction _detectionResourceAction;
        private MoveToPositionState _moveToPositionState;
        private GatheringResourceState _gatheringResourceState;
        private UnloadingResourceState _unloadingResourceState;
        private StateMachine _stateMachine;
        
        private void Start()
        {
            _stateMachine = new StateMachine();

            _moveToPositionState =
                new MoveToPositionState(MoveToPositionData, _characterModel.MoveDirection, _characterModel.Root);

            _gatheringResourceState =
                new GatheringResourceState(GatheringResourceData, MoveToPositionData, _moveToPositionState, _characterModel);

            _unloadingResourceState = new UnloadingResourceState(UnloadResourceData, MoveToPositionData, _characterModel, _moveToPositionState);
            
            _stateMachine.SwitchState(_gatheringResourceState);
        }

        private void Update()
        {
            if (_characterModel.Amount.Value == 0)
            {
                _stateMachine.SwitchState(_gatheringResourceState);
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