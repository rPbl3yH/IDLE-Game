using Modules.Atomic.Values;
    
namespace App.Gameplay.Resource.Model
{
    public class HideResourceMechanics
    {
        private readonly IAtomicVariable<bool> _isHide;
        private readonly IAtomicVariable<int> _amount;

        public HideResourceMechanics(IAtomicVariable<bool> isHide, IAtomicVariable<int> amount)
        {
            _isHide = isHide;
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
            _isHide.Value = obj == 0;
        }
    }
}