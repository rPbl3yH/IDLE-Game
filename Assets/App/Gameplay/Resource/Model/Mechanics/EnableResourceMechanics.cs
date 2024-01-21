using Atomic.Elements;

namespace App.Gameplay.Resource.Model
{
    public class EnableResourceMechanics
    {
        private readonly IAtomicVariable<bool> _isEnabled;
        private readonly IAtomicObservable<int> _amount;

        public EnableResourceMechanics(IAtomicVariable<bool> isEnabled, IAtomicObservable<int> amount)
        {
            _isEnabled = isEnabled;
            _amount = amount;
        }

        public void OnEnable()
        {
            _amount.Subscribe(AmountOnChanged); 
        }

        public void OnDisable()
        {
            _amount.Unsubscribe(AmountOnChanged);
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