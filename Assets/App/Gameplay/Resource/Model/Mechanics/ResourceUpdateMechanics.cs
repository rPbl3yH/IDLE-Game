using Atomic.Elements;
using Modules.Elementary.Time.Base;

namespace App.Gameplay.Resource.Model
{
    public class ResourceUpdateMechanics
    {
        private readonly AtomicVariable<float> _updateTime;
        private readonly IAtomicVariable<bool> _isEnable;
        private readonly AtomicVariable<int> _amount;
        private readonly IAtomicValue<int> _maxAmount;

        private readonly Timer _timer;
        
        public ResourceUpdateMechanics(
            IAtomicValue<int> maxAmount,
            AtomicVariable<int> amount,
            AtomicVariable<float> updateTime, 
            IAtomicVariable<bool> isEnable)
        {
            _maxAmount = maxAmount;
            _amount = amount;
            _updateTime = updateTime;
            _isEnable = isEnable;
            _timer = new Timer(updateTime.Value);
        }

        public void OnEnable()
        {
            _amount.Subscribe(AmountOnOnChanged);
            _updateTime.Subscribe(UpdateTimeOnOnChanged);
        }

        private void AmountOnOnChanged(int value)
        {
            if (value == 0)
            {
                Run();
                return;
            }
            
            if (_timer.IsPlaying)
            {
                _timer.Stop();
                _timer.ResetTime();
            }
        }

        public void OnDisable()
        {
            _updateTime.Unsubscribe(UpdateTimeOnOnChanged);
        }

        private void Run()
        {
            _timer.ResetTime();
            _timer.Play();
            _timer.OnFinished += TimerOnFinished;
        }

        private void UpdateTimeOnOnChanged(float value)
        {
            _timer.Duration = value;
        }

        private void TimerOnFinished()
        {
            _timer.OnFinished -= TimerOnFinished;
            _amount.Value = _maxAmount.Value;
            _isEnable.Value = true;
        }
    }
}