using Modules.Atomic.Values;

namespace App.Gameplay.Character.Scripts.Model.Mechanics
{
    public class FreeSpaceResourceMechanic
    {
        private readonly IAtomicVariable<bool> _isFreeSpace;
        private readonly IAtomicVariable<int> _amount;

        private readonly IAtomicValue<int> _maxAmount;

        public FreeSpaceResourceMechanic(IAtomicVariable<bool> isFreeSpace, IAtomicVariable<int> amount, IAtomicValue<int> maxAmount)
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