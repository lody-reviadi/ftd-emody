namespace Game.Logic.State
{
    public class SetCookieState : GameState
    {
        public SetCookieState(GamePresenter managedGame) : base(managedGame)
        {
        }

        public override void EnterState()
        {
            base.EnterState();
            
            game.View.OnEmodyAnimationEnd.AddListener(NextState);
            
            FinalizeCookieOnBoard();
        }

        private void FinalizeCookieOnBoard()
        {
            var dropData = game.model.Drop.Value;

            var isSafelySet = true;

            var centerCol = dropData.Column;
            var centerRow = dropData.Row;
            isSafelySet &= game.model.SetCookieOnBoard(centerCol, centerRow, dropData.CenterId);

            var side1Col = dropData.Side1Column;
            var side1Row = dropData.Side1Row;
            isSafelySet &= game.model.SetCookieOnBoard(side1Col, side1Row, dropData.Side1Id);
            
            var side2Col = dropData.Side2Column;
            var side2Row = dropData.Side2Row;
            isSafelySet &= game.model.SetCookieOnBoard(side2Col, side2Row, dropData.Side2Id);
            
            game.SetControlActive(false);

            if (isSafelySet)
            {
                NextState();
                return;
            }
            
            game.View.SetEatAnimation();
            game.AudioManager.Play("Thud");
        }

        private void NextState()
        {
            game.SetState(new CheckBoardState(game));
        }

        public override void ExitState()
        {
            game.View.OnEmodyAnimationEnd.RemoveListener(NextState);
            base.ExitState();
        }
    }
}