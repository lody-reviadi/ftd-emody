namespace Game.Logic.State
{
    public class InitState : State.GameState
    {
        public InitState(GamePresenter managedGame) : base(managedGame)
        {
        }

        public override void EnterState()
        {
            base.EnterState();
            
            game.model.InitGameModel();
            
            game.SetState(new SetupDropState(game));
        }
    }
}