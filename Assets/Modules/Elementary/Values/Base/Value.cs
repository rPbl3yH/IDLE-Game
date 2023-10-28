using Sirenix.OdinInspector;

namespace Modules.Elementary.Values.Base
{
    public sealed class Value<T> : IValue<T>
    {
        [ShowInInspector, ReadOnly]
        public T Current { get; }

        public Value(T value)
        {
            this.Current = value;
        }
    }
}