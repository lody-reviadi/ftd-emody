namespace Game.Logic.State
{
    public class InitState : GameState
    {
        public InitState(GamePresenter managedGame) : base(managedGame)
        {
        }

        public override void EnterState()
        {
            base.EnterState();
            
            game.model.InitGameModel();
            
            game.View.OnGameStartNotificationEnd.AddListener(OnNotificationEnd);
            
            game.View.ShowGameStartNotification();
        }
        
        private void OnNotificationEnd()
        {
            game.SetState(new SetupDropState(game));
        }
        
        public override void ExitState()
        {
            game.View.OnGameStartNotificationEnd.RemoveListener(OnNotificationEnd);
            base.ExitState();
        }
    }
}