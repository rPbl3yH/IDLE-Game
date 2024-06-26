using UnityEngine;

namespace Modules.Elementary.Values.ScriptableObjects
{
    [CreateAssetMenu(
        fileName = "Scriptable Int",
        menuName = "Elementary/Values/New Scriptable Int"
    )]
    public sealed class ScriptableInt : ScriptableObject, IValue<int>
    {
        public int Current
        {
            get { return this.value; }
        }

        [SerializeField]
        private int value;
    }
}