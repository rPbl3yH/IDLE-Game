using System;
using Modules.Elementary.Values;

namespace Modules.Variables
{
    public interface IVariable<T> : IValue<T>
    {
        event Action<T> OnValueChanged;

        new T Current { get; set; }
    }
}