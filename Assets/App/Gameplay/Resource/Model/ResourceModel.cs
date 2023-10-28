using App.Gameplay.Resource.Model.Mechanics;
using Modules.Atomic.Actions;
using Modules.Atomic.Values;
using UnityEngine;

namespace App.Gameplay.Resource.Model
{
    public class ResourceModel : MonoBehaviour
    {
        public ResourceType ResourceType;
        
        public AtomicVariable<int> Amount;
        public AtomicVariable<int> MaxAmount = new(10);
        public AtomicEvent<int> Gathered;

        private GatheringMechanics _gatheringMechanics;

        private void Awake()
        {
            Amount.Value = MaxAmount.Value;
            _gatheringMechanics = new GatheringMechanics(Gathered, Amount);
        }

        private void OnEnable()
        {
            _gatheringMechanics.OnEnable();
        }

        private void OnDisable()
        {
            _gatheringMechanics.OnDisable();
        }
    }
}