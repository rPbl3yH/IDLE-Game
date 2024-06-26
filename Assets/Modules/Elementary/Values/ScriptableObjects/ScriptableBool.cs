using UnityEngine;

namespace Modules.Elementary.Values.ScriptableObjects
{
    [CreateAssetMenu(
        fileName = "Scriptable Bool",
        menuName = "Elementary/Values/New Scriptable Bool"
    )]
    public sealed class ScriptableBool : ScriptableObject, IValue<bool>
    {
        public bool Current
        {
            get { return this.value; }
        }

        [SerializeField]
        private bool value;
    }
}