namespace Game.Logic.GameState
{
    public interface IGameState
    {
        public void EnterState();
        public void FixedUpdate();
        public void ExitState();
    }
    
    public abstract class GameState : IGameState
    {
        protected GameLogic game;

        protected GameState(GameLogic managedGame)
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