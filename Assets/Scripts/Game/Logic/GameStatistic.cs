using Game.Logic.GameState;
using Game.Object;
using UniRx;
using UnityEngine.Events;

namespace Game.Logic
{
    public class GameStatistic
    {
        public IReadOnlyReactiveProperty<int> Score => _score;

        private readonly IntReactiveProperty _score;

        public IReadOnlyReactiveProperty<int> Stage => _stage;
        private readonly  IntReactiveProperty _stage;
        
        public IReadOnlyReactiveCollection<int> Board => _board;
        private readonly ReactiveCollection<int> _board;

        public DropData Drop { get; private set; }
        public readonly UnityEvent<DropData> onDropDataUpdated = new();

        public void InitGameStatistic()
        {
            _stage.Value = 1;
            _score.Value = 0;
        }

        public void GenerateDrop()
        {
            SetupDropData(DropData.GetRandomData());
        }

        public void SetupDropData(DropData newDropData)
        {
            Drop = newDropData;
            onDropDataUpdated.Invoke(Drop);
        }

        public void UpdateDropPosition(int column, int row)
        {
            
        }

        public void ChangeDropShape()
        {
            Drop.ChangeShape();
        }

        public void RotateDropCW()
        {
            Drop.RotateCW();
        }

        public void RotateDropCCW()
        {
            Drop.RotateCCW();
        }
        
        public void UpdateScore(int add)
        {
            _score.Value += add;
        }

        public void NextStage()
        {
            _stage.Value++;
        }
    }
}