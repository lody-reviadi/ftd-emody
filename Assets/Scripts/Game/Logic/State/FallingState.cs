using UnityEngine;

namespace Game.Logic.State
{
    public class FallingState : GameState
    {
        public FallingState(GamePresenter managedGame) : base(managedGame)
        {
        }

        private float _countdownBeforeFall;
        private float _countdown;
        public override void EnterState()
        {
            base.EnterState();

            _countdownBeforeFall = game.model.GetGameTick();
            _countdown = _countdownBeforeFall;
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            _countdown -= Time.fixedDeltaTime;

            if (_countdown > 0f)
            {
                return;
            }
            
            var currentDepth = game.model.DropDown();

            if (currentDepth > 0)
            {
                _countdown = _countdownBeforeFall;
                return;
            }

            game.SetState(new SetupDropState(game));
        }
    }
}