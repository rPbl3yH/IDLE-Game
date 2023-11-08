using Modules.Atomic.Actions;
using UnityEngine;

namespace App.Gameplay.Resource.View
{
    public class ResourceVFXMechanics
    {
        private ParticleSystem _vfx;
        private AtomicEvent<int> _gathered;

        public ResourceVFXMechanics(ParticleSystem vfx, AtomicEvent<int> gathered)
        {
            _vfx = vfx;
            _gathered = gathered;
        }

        public void OnEnable()
        {
            _gathered.AddListener(OnGathered);
        }

        private void OnGathered(int value)
        {
            _vfx.Play();
        }

        public void OnDisable()
        {
            
        }
    }
}