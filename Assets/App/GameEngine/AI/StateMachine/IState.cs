namespace App.GameEngine.AI.StateMachine
{
    public interface IState
    {
        void Enter();
        
        void Update(float deltaTime);
        
        void Exit();
    }
}