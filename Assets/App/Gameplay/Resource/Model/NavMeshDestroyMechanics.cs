using Modules.Atomic.Values;
using UnityEngine.AI;

namespace App.Gameplay.Resource.Model
{
    public class NavMeshDestroyMechanics
    {
        private readonly NavMeshObstacle _obstacle;
        private readonly IAtomicVariable<bool> _isHide;

        public NavMeshDestroyMechanics(NavMeshObstacle obstacle, IAtomicVariable<bool> isHide)
        {
            _obstacle = obstacle;
            _isHide = isHide;
        }

        public void OnEnable()
        {
            _isHide.OnChanged += IsHideOnChanged;
        }

        public void OnDisable()
        {
            _isHide.OnChanged -= IsHideOnChanged;
        }

        private void IsHideOnChanged(bool isHide)
        {
            _obstacle.enabled = !isHide;
        }
    }
}