using Atomic;

namespace App.Gameplay
{
    public class FreeSpaceResourceMechanic
    {
        private readonly AtomicVariable<bool> _isFreeSpace;
        private readonly AtomicVariable<int> _amount;

        private readonly AtomicVariable<int> _maxAmount;

        public FreeSpaceResourceMechanic(AtomicVariable<bool> isFreeSpace, AtomicVariable<int> amount, AtomicVariable<int> maxAmount)
        {
            _isFreeSpace = isFreeSpace;
            _amount = amount;
            _maxAmount = maxAmount;
            AmountOnChanged(_amount.Value);
        }

        public void OnEnable()
        {
            _amount.OnChanged += AmountOnChanged;
        }

        public void OnDisable()
        {
            _amount.OnChanged -= AmountOnChanged;
        }

        private void AmountOnChanged(int value)
        {
            _isFreeSpace.Value = value < _maxAmount.Value;
        }
    }
}