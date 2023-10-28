namespace Modules.Atomic.Actions
{
    public interface IAtomicFunction<out T>
    {
        T GetResult();
    }
    
    public interface IAtomicAction
    {
        void Invoke();
    }

    public interface IAtomicAction<in T>
    {
        void Invoke(T args);
    }
}