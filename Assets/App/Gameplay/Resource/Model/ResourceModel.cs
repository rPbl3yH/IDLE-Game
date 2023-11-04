using App.Gameplay.Resource.Model.Mechanics;
using Modules.Atomic.Actions;
using Modules.Atomic.Values;
using UnityEngine;
using UnityEngine.AI;

namespace App.Gameplay.Resource.Model
{
    public class ResourceModel : MonoBehaviour
    {
        public ResourceType ResourceType;
        
        public AtomicVariable<int> Amount;
        public AtomicVariable<int> MaxAmount = new(10);
        public AtomicEvent<int> Gathered;
        public AtomicVariable<bool> IsHide;

        public NavMeshObstacle Obstacle;

        private GatheringMechanics _gatheringMechanics;
        private HideResourceMechanics _hideResourceMechanics;
        private NavMeshDestroyMechanics _destroyMechanics;
        
        private void Awake()
        {
            Amount.Value = MaxAmount.Value;
            _gatheringMechanics = new GatheringMechanics(Gathered, Amount);
            _hideResourceMechanics = new HideResourceMechanics(IsHide, Amount);
            _destroyMechanics = new NavMeshDestroyMechanics(Obstacle, IsHide);
        }

        private void OnEnable()
        {
            _gatheringMechanics.OnEnable();
            _hideResourceMechanics.OnEnable();
            _destroyMechanics.OnEnable();
        }

        private void OnDisable()
        {
            _gatheringMechanics.OnDisable();
            _hideResourceMechanics.OnDisable();
            _destroyMechanics.OnDisable();
        }
    }
}