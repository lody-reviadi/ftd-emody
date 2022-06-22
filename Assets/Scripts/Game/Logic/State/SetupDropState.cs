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
            
            game.model.GenerateRandomDropData();
            game.View.DropObject.gameObject.SetActive(true);
            
            game.SetState(new FallingState(game));
        }
    }
}