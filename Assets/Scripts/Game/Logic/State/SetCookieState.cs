using UnityEngine;

namespace Game.Logic.State
{
    public class SetCookieState : GameState
    {
        public SetCookieState(GamePresenter managedGame) : base(managedGame)
        {
        }

        private float _cooldown = 1f;
        
        public override void FixedUpdate()
        {
            base.FixedUpdate();

            _cooldown -= Time.fixedDeltaTime;

            if (_cooldown > 0f)
            {
                return;
            }
            
            FinalizeCookieOnBoard();
            
            game.SetState(new CheckBoardState(game));
        }

        private void FinalizeCookieOnBoard()
        {
            var dropData = game.model.Drop.Value;

            var centerCol = dropData.Column;
            var centerRow = dropData.Row;
            game.model.SetCookieOnBoard(centerCol, centerRow, dropData.CenterId);

            var side1Col = dropData.Side1Column;
            var side1Row = dropData.Side1Row;
            game.model.SetCookieOnBoard(side1Col, side1Row, dropData.Side1Id);
            
            var side2Col = dropData.Side2Column;
            var side2Row = dropData.Side2Row;
            game.model.SetCookieOnBoard(side2Col, side2Row, dropData.Side2Id);
        }
    }
}