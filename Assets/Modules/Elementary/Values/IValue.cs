namespace Modules.Elementary.Values
{
    public interface IValue<out T>
    {
        T Current { get; }
    }
}