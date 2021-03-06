namespace Game.Logic.State
{
    public class GameOverState : GameState
    {
        public GameOverState(GamePresenter managedGame) : base(managedGame)
        {
        }

        public override void EnterState()
        {
            base.EnterState();
            
            game.AudioManager.Play("GameOver");
            game.View.SetGameOver();
        }
    }
}