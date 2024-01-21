using App.Gameplay.Character.Scripts.Model;
using App.Gameplay.Character.Scripts.Model.Actions;
using App.Gameplay.LevelStorage;
using App.Gameplay.Resource;
using UnityEngine;
using VContainer;

namespace App.GameEngine.AI
{
    public class CharacterAIModel : MonoBehaviour
    {
        [SerializeField] private CharacterModel _characterModel;
        
        public MoveToPositionData MoveToPositionData;
        public GatheringResourceData GatheringResourceData;
        public UnloadResourceData UnloadResourceData;

        private DetectionResourceFunction _detectionResourceFunction;
        private MoveToPositionState _moveToPositionState;
        private GatheringResourceState _gatheringResourceState;
        private UnloadingResourceState _unloadingResourceState;
        private StateMachine _stateMachine;
        public DetectionBarnFunction DetectionBarnFunction;

        [Inject]
        public void Construct(BarnService barnService, ResourceService resourceService)
        {
            UnloadResourceData.BarnService = barnService;
            _characterModel.Construct(resourceService);
        }
        
        private void Start()
        {
            DetectionBarnFunction = new DetectionBarnFunction(UnloadResourceData.BarnService);

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
            if (_characterModel.ResourceAmount.Value == 0)
            {
                _stateMachine.SwitchState(_gatheringResourceState);
            }
            else
            {
                _characterModel.ResourceStorage.Value = DetectionBarnFunction?.Invoke();
                
                if (!_characterModel.IsFreeSpace.Value && _characterModel.ResourceStorage.Value != null)
                {
                    _stateMachine.SwitchState(_unloadingResourceState);
                }
            }
            
            _stateMachine.Update(Time.deltaTime);
        }
    }
}