namespace Game.Logic.State
{
    public class SetupDropState : GameState
    {
        public SetupDropState(GamePresenter managedGame) : base(managedGame)
        {
            
        }

        public override void EnterState()
        {
            base.EnterState();

            if (game.model.CookieCount.Value <= 0)
            {
                game.SetState(new GameOverState(game));
                return;
            }
            
            game.View.SetThrowAnimation();
            
            game.SetControlActive(true);
            
            game.model.GenerateRandomDropData();
            game.View.DropObject.gameObject.SetActive(true);
            
            game.SetState(new FallingState(game));
        }
    }
}