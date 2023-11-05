using Modules.Atomic.Values;
using Modules.Elementary.Time.Base;

namespace App.Gameplay.Resource.Model
{
    public class ResourceUpdateMechanics
    {
        private readonly IAtomicVariable<float> _updateTime;
        private readonly IAtomicVariable<bool> _isEnable;
        private readonly IAtomicVariable<int> _amount;
        private readonly IAtomicValue<int> _maxAmount;

        private readonly Timer _timer;
        
        public ResourceUpdateMechanics(
            IAtomicValue<int> maxAmount,
            IAtomicVariable<int> amount,
            IAtomicVariable<float> updateTime, 
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
            _updateTime.OnChanged += UpdateTimeOnOnChanged;
            _isEnable.OnChanged += IsEnableOnOnChanged;
        }

        public void OnDisable()
        {
            _updateTime.OnChanged -= UpdateTimeOnOnChanged;
            _isEnable.OnChanged -= IsEnableOnOnChanged;
        }

        private void Run()
        {
            _timer.ResetTime();
            _timer.Play();
            _timer.OnFinished += TimerOnFinished;
        }

        private void IsEnableOnOnChanged(bool isEnable)
        {
            if (!isEnable)
            {
                Run();
            }
            else
            {
                if (_timer.IsPlaying)
                {
                    _timer.Stop();
                    _timer.ResetTime();
                }
            }
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