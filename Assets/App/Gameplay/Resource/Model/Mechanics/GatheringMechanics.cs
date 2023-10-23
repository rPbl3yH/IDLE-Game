using Atomic;

namespace App.Gameplay.Resource
{
    public class GatheringMechanics
    {
        private readonly AtomicEvent<int> _gathered;
        private readonly AtomicVariable<int> _amount;

        public GatheringMechanics(AtomicEvent<int> gathered, AtomicVariable<int> amount)
        {
            _gathered = gathered;
            _amount = amount;
        }

        public void OnEnable()
        {
            _gathered.AddListener(OnGathered);
        }

        public void OnDisable()
        {
            _gathered.RemoveListener(OnGathered);
        }
        
        private void OnGathered(int count)
        {
            _amount.Value -= count;
            //Debug.Log($"Gathered {count}");
        }
    }
}