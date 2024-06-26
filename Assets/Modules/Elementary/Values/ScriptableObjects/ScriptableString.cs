using UnityEngine;

namespace Modules.Elementary.Values.ScriptableObjects
{
    [CreateAssetMenu(
        fileName = "Scriptable String",
        menuName = "Elementary/Values/New Scriptable String"
    )]
    public sealed class ScriptableString : ScriptableObject, IValue<string>
    {
        public string Current
        {
            get { return this.value; }
        }

        [SerializeField]
        private string value;
    }
}