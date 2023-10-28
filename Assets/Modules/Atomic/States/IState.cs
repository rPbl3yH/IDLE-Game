namespace Modules.Atomic.States
{
    public interface IState
    {
        void Enter();

        void Exit();
    }
}