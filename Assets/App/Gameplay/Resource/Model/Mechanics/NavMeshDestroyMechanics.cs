using Atomic.Elements;
using UnityEngine.AI;

namespace App.Gameplay.Resource.Model
{
    public class NavMeshDestroyMechanics
    {
        private readonly NavMeshObstacle _obstacle;
        private readonly IAtomicObservable<bool> _isEnable;

        public NavMeshDestroyMechanics(NavMeshObstacle obstacle, IAtomicObservable<bool> isEnable)
        {
            _obstacle = obstacle;
            _isEnable = isEnable;
        }

        public void OnEnable()
        {
            _isEnable.Subscribe(IsEnableOnChanged);
        }

        public void OnDisable()
        {
            _isEnable.Unsubscribe(IsEnableOnChanged);
        }

        private void IsEnableOnChanged(bool isEnable)
        {
            _obstacle.enabled = isEnable;
        }
    }
}