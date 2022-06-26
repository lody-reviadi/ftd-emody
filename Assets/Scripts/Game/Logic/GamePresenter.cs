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
        public IAudioManager audioManager => _audioManager;
        
        public readonly GameModel model = new();
        
        private IGameState _currentState;

        private bool _isControlActive = false;
        
        private void Start()
        {
            BindModelProperties();
            BindViewCallbacks();
            
            _audioManager.PlayBGM("Emoja");
            
            SetState(new InitState(this));
        }

        private void BindModelProperties()
        {
            model.CookieCount.Subscribe((updatedCount) =>
            {
                view.UpdateCookieCounter(updatedCount);
            }).AddTo(this);

            model.Stage.Subscribe((updatedStage) =>
            {
                view.UpdateStageCounter(updatedStage);
            }).AddTo(this);

            model.Drop.Subscribe((dropData) =>
            {
                view.DropObject.Bind(dropData);
            }).AddTo(this);

            model.Board.ObserveReplace().Subscribe((evt) =>
            {
                view.SetCookieSprites(evt.Index, evt.NewValue);
            }).AddTo(this);

            model.Board.ObserveAdd().Subscribe(evt =>
            {
                view.SetCookieSprites(evt.Index, evt.Value);
            }).AddTo(this);
        }

        private void BindViewCallbacks()
        {
            view.OnChangeShapeButtonClicked.AddListener(ChangeShape);
            view.OnRotateCWButtonClicked.AddListener(RotateCW);
            view.OnRotateCCWButtonClicked.AddListener(RotateCCW);
            view.onGridClickedEvent.AddListener(model.UpdateDropPosition);
            view.OnDropButtonClicked.AddListener(Drop);
        }

        public void SetControlActive(bool isActive)
        {
            _isControlActive = isActive;
            view.DropObject.gameObject.SetActive(isActive);
        }

        private void ChangeShape()
        {
            if (!_isControlActive)
            {
                return;
            }
            
            _audioManager.Play("Change");
            model.ChangeDropShape();
        }

        private void RotateCW()
        {
            if (!_isControlActive)
            {
                return;
            }
            
            _audioManager.Play("RotateCW");
            model.RotateDropCW();
        }

        private void RotateCCW()
        {
            if (!_isControlActive)
            {
                return;
            }
            
            _audioManager.Play("RotateCCW");
            model.RotateDropCCW();
        }

        private void Drop()
        {
            if (!_isControlActive)
            {
                return;
            }
            
            _audioManager.Play("Drop");
            model.InstantDrop();
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