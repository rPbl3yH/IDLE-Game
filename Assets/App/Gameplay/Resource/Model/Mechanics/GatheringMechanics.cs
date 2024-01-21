using Atomic.Elements;

namespace App.Gameplay.Resource.Model.Mechanics
{
    public class GatheringMechanics
    {
        private readonly AtomicEvent<int> _gathered;
        private readonly IAtomicVariable<int> _amount;

        public GatheringMechanics(AtomicEvent<int> gathered, IAtomicVariable<int> amount)
        {
            _gathered = gathered;
            _amount = amount;
        }

        public void OnEnable()
        {
            _gathered.Subscribe(OnGathered);
        }

        public void OnDisable()
        {
            _gathered.Unsubscribe(OnGathered);
        }
        
        private void OnGathered(int count)
        {
            _amount.Value -= count;
            //Debug.Log($"Gathered {count}");
        }
    }
}