namespace App.Gameplay.AI.States
{
    public interface IState
    {
        void Enter();
        
        void Update(float deltaTime);
        
        void Exit();
    }
}