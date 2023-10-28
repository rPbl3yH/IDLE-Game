using System;

namespace Modules.Variables
{
    public interface IVariableLimited<T> : IVariable<T>
    {
        event Action<T> OnMaxValueChanged;
        
        T MaxValue { get; set; }
        
        bool IsLimit { get; }
    }
}