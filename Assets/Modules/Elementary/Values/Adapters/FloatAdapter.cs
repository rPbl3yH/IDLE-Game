using System;
using System.Runtime.Serialization;
using Modules.Elementary.Values.ScriptableObjects;
using Modules.Variables.MonoBehaviours;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Modules.Elementary.Values.Adapters
{
    [Serializable]
    public sealed class FloatAdapter : IValue<float>
    {
        public float Current
        {
            get { return this.GetValue(); }
        }

        [SerializeField]
        public Mode mode;

        [ShowIf("mode", Mode.BASE)]
        [SerializeField]
        public float baseValue;

        [ShowIf("mode", Mode.MONO_BEHAVIOUR)]
        [OptionalField]
        [SerializeField]
        public MonoFloatVariable monoValue;

        [ShowIf("mode", Mode.SCRIPTABLE_OBJECT)]
        [OptionalField]
        [SerializeField]
        public ScriptableFloat scriptableValue;

        public float GetValue()
        {
            return this.mode switch
            {
                Mode.BASE => this.baseValue,
                Mode.MONO_BEHAVIOUR => this.monoValue.Current,
                Mode.SCRIPTABLE_OBJECT => this.scriptableValue.Current,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        public enum Mode
        {
            BASE = 0,
            MONO_BEHAVIOUR = 1,
            SCRIPTABLE_OBJECT = 2
        }
    }
}