using UnityEngine;

namespace Modules.Elementary.Values.ScriptableObjects
{
    [CreateAssetMenu(
        fileName = "Scriptable Float",
        menuName = "Elementary/Values/New Scriptable Float"
    )]
    public sealed class ScriptableFloat : ScriptableObject, IValue<float>
    {
        public float Current
        {
            get { return this.value; }
        }

        [SerializeField]
        private float value;
    }
}