using System;
using Modules.Atomic.Values;
using UnityEngine;

namespace App.Gameplay.Player
{
    [Serializable]
    public class CameraFollowingMechanics
    {
        private Transform _camera;
        private Transform _target;
        private float _speedRate;

        public CameraFollowingMechanics(Transform camera, Transform target, float speedRate)
        {
            _camera = camera;
            _target = target;
            _speedRate = speedRate;
        }

        public void Update(float deltaTime)
        {
            if (_target == null)
            {
                return;
            }

            var targetPosition = new Vector3(_target.position.x, 0f, _target.position.z);
            _camera.position = Vector3.Lerp(_camera.position, targetPosition, _speedRate * deltaTime);
        }
    }
}
