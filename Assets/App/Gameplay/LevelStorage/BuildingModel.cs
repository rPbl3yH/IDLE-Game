using Atomic.Elements;
using UnityEngine;

namespace App.Gameplay
{
    public abstract class BuildingModel : MonoBehaviour
    {
        public AtomicVariable<bool> IsEnable;
        public AtomicEvent Deactivated;
    }
}