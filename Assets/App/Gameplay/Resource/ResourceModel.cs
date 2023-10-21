using Atomic;
using UnityEngine;

namespace App.Gameplay.Resource
{
    public class ResourceModel : MonoBehaviour
    {
        public AtomicVariable<int> Amount;
        public AtomicVariable<int> MaxAmount = new(10);
        public AtomicEvent<int> GatheringRequested;
        public AtomicEvent<int> GatheringSuccess;

        private GatheringMechanics _gatheringMechanics;

        private void Awake()
        {
            _gatheringMechanics = new GatheringMechanics(GatheringRequested, GatheringSuccess, Amount);
        }

        private void OnEnable()
        {
            _gatheringMechanics.OnEnable();
        }

        private void OnDisable()
        {
            _gatheringMechanics.OnDisable();
        }

        private void Start()
        {
            Amount.Value = MaxAmount.Value;
        }
    }
}