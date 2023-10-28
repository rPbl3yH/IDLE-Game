using System;
using Modules.Atomic.Values;
using UnityEngine;

namespace App.Gameplay.Player
{
    [Serializable]
    public class CameraFollowingMechanics
    {
        [SerializeField] private Transform _camera;
        [SerializeField] private Transform _target;
        [SerializeField] private AtomicVariable<float> _speedRate;

        public void Update(float deltaTime)
        {
            if (_target == null)
            {
                return;
            }

            var targetPosition = new Vector3(_target.position.x, 0f, _target.position.z);
            _camera.position = Vector3.Lerp(_camera.position, targetPosition, _speedRate.Value * deltaTime);
        }
    }
}
