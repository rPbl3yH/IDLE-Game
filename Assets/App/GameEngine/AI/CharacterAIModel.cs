using App.GameEngine.AI.StateMachine.Data;
using App.GameEngine.AI.StateMachine.States;
using App.Gameplay.Character.Scripts.Model;
using App.Gameplay.Character.Scripts.Model.Actions;
using App.Gameplay.LevelStorage;
using UnityEngine;

namespace App.GameEngine.AI
{
    public class CharacterAIModel : MonoBehaviour
    {
        public MoveToPositionData MoveToPositionData;
        public GatheringResourceData GatheringResourceData;
        public UnloadResourceData UnloadResourceData;

        [SerializeField] private CharacterModel _characterModel;
            
        private DetectionResourceFunction _detectionResourceFunction;
        private MoveToPositionState _moveToPositionState;
        private GatheringResourceState _gatheringResourceState;
        private UnloadingResourceState _unloadingResourceState;
        private StateMachine.StateMachine _stateMachine;
        public DetectionBarnFunction DetectionBarnFunction;
        
        private void Start()
        {
            DetectionBarnFunction = new DetectionBarnFunction(UnloadResourceData.BarnService);

            _stateMachine = new StateMachine.StateMachine();

            _moveToPositionState =
                new MoveToPositionState(MoveToPositionData, _characterModel.MoveDirection, _characterModel.Root);

            _gatheringResourceState =
                new GatheringResourceState(GatheringResourceData, MoveToPositionData, _moveToPositionState, _characterModel);

            _unloadingResourceState = new UnloadingResourceState(UnloadResourceData, MoveToPositionData, DetectionBarnFunction, _characterModel, _moveToPositionState);
            
            _stateMachine.SwitchState(_gatheringResourceState);
        }

        private void Update()
        {
            if (_characterModel.ResourceAmount.Value == 0)
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