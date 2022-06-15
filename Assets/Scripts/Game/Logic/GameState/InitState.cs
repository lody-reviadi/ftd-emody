namespace Game.Logic.GameState
{
    public class InitState : GameState
    {
        public InitState(GameLogic managedGame) : base(managedGame)
        {
        }

        public override void EnterState()
        {
            base.EnterState();
            
            game.statistic.InitGameStatistic();
            
            game.SetState(new SetupDrop(game));
        }
    }
}