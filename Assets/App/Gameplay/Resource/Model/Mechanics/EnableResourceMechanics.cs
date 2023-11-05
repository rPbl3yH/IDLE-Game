using Modules.Atomic.Values;
    
namespace App.Gameplay.Resource.Model
{
    public class EnableResourceMechanics
    {
        private readonly IAtomicVariable<bool> _isEnabled;
        private readonly IAtomicVariable<int> _amount;

        public EnableResourceMechanics(IAtomicVariable<bool> isEnabled, IAtomicVariable<int> amount)
        {
            _isEnabled = isEnabled;
            _amount = amount;
        }

        public void OnEnable()
        {
            _amount.OnChanged += AmountOnChanged; 
        }

        public void OnDisable()
        {
            _amount.OnChanged -= AmountOnChanged;
        }

        private void AmountOnChanged(int amount)
        {
            if (amount == 0 && _isEnabled.Value)
            {
                _isEnabled.Value = false;
            }
            else if(amount > 0 && !_isEnabled.Value)
            {
                _isEnabled.Value = true;
            }
        }
    }
}