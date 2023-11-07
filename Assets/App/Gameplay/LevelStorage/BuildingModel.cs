using System;
using App.Gameplay.Resource.View;
using Modules.Atomic.Actions;
using Modules.Atomic.Values;
using UnityEngine;

namespace App.Gameplay
{
    public abstract class BuildingModel : MonoBehaviour
    {
        public AtomicVariable<bool> IsEnable;
        public AtomicEvent Deactivated;
    }
}