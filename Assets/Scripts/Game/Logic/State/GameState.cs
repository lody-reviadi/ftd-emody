namespace Game.Logic.State
{
    public interface IGameState
    {
        public void EnterState();
        public void FixedUpdate();
        public void ExitState();
    }
    
    public abstract class GameState : IGameState
    {
        protected GamePresenter game;

        protected GameState(GamePresenter managedGame)
        {
            game = managedGame;
        }
        
        public virtual void EnterState()
        {
            
        }

        public virtual void FixedUpdate()
        {
            
        }

        public virtual void ExitState()
        {
            
        }
    }
}