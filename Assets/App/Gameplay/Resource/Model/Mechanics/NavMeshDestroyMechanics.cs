using Modules.Atomic.Values;
using UnityEngine.AI;

namespace App.Gameplay.Resource.Model
{
    public class NavMeshDestroyMechanics
    {
        private readonly NavMeshObstacle _obstacle;
        private readonly IAtomicVariable<bool> _isEnable;

        public NavMeshDestroyMechanics(NavMeshObstacle obstacle, IAtomicVariable<bool> isEnable)
        {
            _obstacle = obstacle;
            _isEnable = isEnable;
        }

        public void OnEnable()
        {
            _isEnable.OnChanged += IsEnableOnChanged;
        }

        public void OnDisable()
        {
            _isEnable.OnChanged -= IsEnableOnChanged;
        }

        private void IsEnableOnChanged(bool isEnable)
        {
            _obstacle.enabled = isEnable;
        }
    }
}