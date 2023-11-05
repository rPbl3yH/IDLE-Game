using Modules.Atomic.Values;
    
namespace App.Gameplay.Resource.Model
{
    public class HideResourceMechanics
    {
        private readonly IAtomicVariable<bool> _isEnabled;
        private readonly IAtomicVariable<int> _amount;

        public HideResourceMechanics(IAtomicVariable<bool> isEnabled, IAtomicVariable<int> amount)
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

        private void AmountOnChanged(int obj)
        {
            _isEnabled.Value = obj != 0;
        }
    }
}