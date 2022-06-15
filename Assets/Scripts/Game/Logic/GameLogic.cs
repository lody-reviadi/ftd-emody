using Game.Logic.GameState;
using Game.Object;
using UniRx;
using UnityEngine;

namespace Game.Logic
{
    public class GameLogic : MonoBehaviour
    {
        [SerializeField] private DropObject _dropObject;

        public readonly GameStatistic statistic = new GameStatistic();
        
        private GameState.GameState _currentState;

        private void Start()
        {
            statistic.Score.Subscribe((updatedScore) =>
            {

            }).AddTo(this);

            statistic.Stage.Subscribe((updatedStage) =>
            {

            }).AddTo(this);
            
            statistic.onDropDataUpdated.AddListener(_dropObject.Bind);
            
            SetState(new InitState(this));
        }

        public void SetState(GameState.GameState newState)
        {
            _currentState?.ExitState();

            _currentState = newState;
            _currentState?.EnterState();
        }

        private void FixedUpdate()
        {
            _currentState?.FixedUpdate();
        }
    }
}