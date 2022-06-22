using Game.Logic.State;
using UniRx;
using UnityEngine;
using Utilities.Audio;
using Zenject;

namespace Game.Logic
{
    public class GamePresenter : MonoBehaviour
    {
        [SerializeField] private GameView view;
        public GameView View => view;
        
        [Inject]
        private IAudioManager _audioManager;
        
        public readonly GameModel model = new();
        
        private IGameState _currentState;
        
        private void Start()
        {
            BindModelProperties();
            BindViewCallbacks();
            
            _audioManager.PlayBGM("Emoja");
            
            SetState(new InitState(this));
        }

        private void BindModelProperties()
        {
            model.Score.Subscribe((updatedScore) =>
            {

            }).AddTo(this);

            model.CookieCount.Subscribe((updatedCount) =>
            {
                view.UpdateCookieCounter(updatedCount);
            }).AddTo(this);

            model.Stage.Subscribe((updatedStage) =>
            {

            }).AddTo(this);

            model.Drop.Subscribe((dropData) =>
            {
                view.DropObject.Bind(dropData);
            }).AddTo(this);

            model.Board.ObserveReplace().Subscribe((evt) =>
            {
                view.SetCookieSprites(evt.Index, evt.NewValue);
            }).AddTo(this);
        }

        private void BindViewCallbacks()
        {
            view.OnChangeShapeButtonClicked.AddListener(model.ChangeDropShape);
            view.OnRotateCWButtonClicked.AddListener(model.RotateDropCW);
            view.OnRotateCCWButtonClicked.AddListener(model.RotateDropCCW);
            view.onGridClickedEvent.AddListener(model.UpdateDropPosition);
            view.OnDropButtonClicked.AddListener(model.InstantDrop);
        }

        public void SetState(GameState newState)
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