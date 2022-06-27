using Game.Helper;
using Game.Object;
using UniRx;
using UnityEngine;

namespace Game.Logic
{
    public class GameModel
    {
        public int Column { get; private set; }

        public int Row { get; private set; }

        public IReadOnlyReactiveProperty<int> CookieCount => _cookieCount;
        private readonly IntReactiveProperty _cookieCount = new();
        
        public IReadOnlyReactiveProperty<int> Stage => _stage;
        private readonly  IntReactiveProperty _stage = new();
        
        public IReadOnlyReactiveCollection<int> Board => _board;
        private readonly ReactiveCollection<int> _board = new();

        private int _variance = 4;
        private const int InitialDepth = 10;

        public IReadOnlyReactiveProperty<DropData> Drop => _drop;
        private readonly ReactiveProperty<DropData> _drop = new(new DropData());

        public void InitGameModel()
        {
            _stage.Value = 1;
            
            Column = 5;
            Row = 5;

            _variance = PlayerPrefs.GetInt("Variance", 4);
            
            SetStageVariables();
        }

        private void SetStageVariables()
        {
            _cookieCount.Value = Mathf.Max((50 - (_stage.Value - 1) * 5), 10);
            
            _board.Clear();
            for (var i = 0; i < Column * Row; i++)
            {
                _board.Add(-1);   
            }
            
            _board[12] = Random.Range(0, _variance);
        }

        public void GenerateRandomDropData()
        {
            if (_cookieCount.Value <= 0)
            {
                return;
            }
            
            _cookieCount.Value--;
            
            var cookie1 = Random.Range(0, _variance);
            var cookie2 = Random.Range(0, _variance);
            var cookie3 = Random.Range(0, _variance);

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

        public void InstantDrop()
        {
            _drop.Value.InstantDrop();
        }

        public bool SetCookieOnBoard(int col, int row, int cookieType)
        {
            var index = col + row * Column;

            if ((_board[index] >= 0) && (cookieType >=0))
            {
                Debug.LogError(_board[index]+" "+cookieType+" "+index);
                _board[index] = -1;
                return false;
            }

            _board[index] = cookieType;
            return true;
        }

        public void ClearCookie(int index)
        {
            _board[index] = -1;
        }

        public int GetCookieType(int col, int row)
        {
            if ((col >= Column) || (row >= Row))
            {
                return -1;
            }

            return _board[col + row * Column];
        }

        public void NextStage()
        {
            _stage.Value++;
            
            SetStageVariables();
        }
        
        public float GetGameTick()
        {
            return 1f - (_stage.Value - 1) * 0.025f;
        }
    }
}