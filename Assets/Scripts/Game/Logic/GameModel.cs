using Game.Helper;
using Game.Object;
using UniRx;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Logic
{
    public class GameModel
    {
        public IReadOnlyReactiveProperty<int> Score => _score;

        private readonly IntReactiveProperty _score = new();

        public IReadOnlyReactiveProperty<int> Stage => _stage;
        private readonly  IntReactiveProperty _stage = new();
        
        public IReadOnlyReactiveCollection<int> Board => _board;
        private readonly ReactiveCollection<int> _board = new();

        private const int Variance = 4;
        private const int InitialDepth = 10;

        public IReadOnlyReactiveProperty<DropData> Drop => _drop;
        private readonly ReactiveProperty<DropData> _drop = new(new DropData());

        public void InitGameModel()
        {
            _stage.Value = 1;
            _score.Value = 0;
        }

        public void GenerateRandomDropData()
        {
            var cookie1 = Random.Range(0, Variance);
            var cookie2 = Random.Range(0, Variance);
            var cookie3 = Random.Range(0, Variance);
            
            var randomIndex1 = Random.Range(0, 4);
            var randomIndex2 = (randomIndex1 + Random.Range(1, 4)) % 4;
            
            var side1Pos = (PositionType)randomIndex1;
            var side2Pos = (PositionType)randomIndex2;
            
            Drop.Value.Initialize(cookie1, cookie2, cookie3, side1Pos, side2Pos, InitialDepth);
        }

        public void UpdateDropPosition(int column, int row)
        {
            Drop.Value.UpdatePosition(column, row);
        }

        public void ChangeDropShape()
        {
            Drop.Value.ChangeShape();
        }

        public void RotateDropCW()
        {
            Drop.Value.RotateCW();
        }

        public void RotateDropCCW()
        {
            Drop.Value.RotateCCW();
        }

        public int DropDown()
        {
            _drop.Value.DropDown();
            return _drop.Value.Depth;
        }
        
        public void UpdateScore(int add)
        {
            _score.Value += add;
        }

        public void NextStage()
        {
            _stage.Value++;
        }

        public float GetGameTick()
        {
            return 1f - (_stage.Value - 1) * 0.02f;
        }
    }
}