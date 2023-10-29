using System;
using Modules.Atomic.Values;
using UnityEngine;

namespace App.Gameplay.Player
{
    public class DistanceSensor
    {
        public event Action Entered;

        public event Action Exited;

        private bool _isInside;

        private readonly IAtomicValue<float> _distance;

        private readonly Transform _firstPoint;

        private readonly Transform _secondPoint;

        public DistanceSensor(Transform firstPoint, Transform secondPoint, IAtomicValue<float> distance)
        {
            _firstPoint = firstPoint;
            _secondPoint = secondPoint;
            _distance = distance;
        }

        public void Update()
        {
            if (_firstPoint == null || _secondPoint == null)
            {
                return;
            }
            
            var distance = Vector3.Distance(_firstPoint.position, _secondPoint.position);
            
            if (distance <= _distance.Value)
            {
                if (!_isInside)
                {
                    //Debug.Log("Entered");
                    _isInside = true;
                    Entered?.Invoke();
                }
            }
            else
            {
                if (_isInside)
                {
                    //Debug.Log("Exited");
                    _isInside = false;
                    Exited?.Invoke();                    
                }
            }
        }
    }
}