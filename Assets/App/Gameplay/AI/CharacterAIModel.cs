using App.Gameplay.AI.Model;
using App.Gameplay.AI.States;
using UnityEngine;

namespace App.Gameplay.AI
{
    public class CharacterAIModel : MonoBehaviour
    {
        public MoveToPositionData MoveToPositionData;
        public DetectionResourceData DetectionResourceData;

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

            _unloadingResourceState = new UnloadingResourceState(_characterModel);
            
            _stateMachine.SwitchState(_detectionResourceState);
        }

        private void Update()
        {
            _stateMachine.Update(Time.deltaTime);
            _unloadingResourceState.Update(Time.deltaTime);
        }
    }
}