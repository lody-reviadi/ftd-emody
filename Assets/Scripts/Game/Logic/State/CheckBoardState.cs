using System.Collections.Generic;
using System.Linq;

namespace Game.Logic.State
{
    public class CheckBoardState : GameState
    {
        public CheckBoardState(GamePresenter managedGame) : base(managedGame)
        {
        }

        private readonly List<int> _groupedIndex = new();

        public override void EnterState()
        {
            base.EnterState();

            _groupedIndex.Clear();

            if (!CheckForGroups() 
                && CheckGameClear())
            {
                game.model.NextStage();
            }
            
            game.SetState(new SetupDropState(game));
        }

        private bool CheckForGroups()
        {
            CheckHorizontalGroups();
            CheckVerticalGroups();

            if (_groupedIndex.Count <= 0)
            {
                return false;
            }
            
            foreach (var i in _groupedIndex)
            {
                game.model.ClearCookie(i);
            }

            return true;
        }

        private bool CheckGameClear()
        {
            return !game.model.Board.Contains(-1);
        }

        private void CheckHorizontalGroups()
        {
            var checkList = new List<int>();

            var totalColumn = game.model.Column;
            var totalRow = game.model.Row;

            for (var row = 0; row < totalRow; row++)
            {
                var checkedCookieType = -1;

                for (var col = 0; col < totalColumn; col++)
                {
                    var cookieType = game.model.GetCookieType(col, row);
                    if (checkedCookieType != cookieType)
                    {
                        if (checkList.Count >= 3)
                        {
                            _groupedIndex.AddRange(checkList);
                        }

                        checkedCookieType = cookieType;
                        checkList.Clear();
                    }

                    if (cookieType == -1)
                    {
                        continue;
                    }

                    checkList.Add(col + row * totalColumn);
                }
                
                if (checkList.Count >= 3)
                {
                    _groupedIndex.AddRange(checkList);
                }

                checkList.Clear();
            }
        }
        
        private void CheckVerticalGroups()
        {
            var checkList = new List<int>();

            var totalColumn = game.model.Column;
            var totalRow = game.model.Row;

            for (var col = 0; col < totalColumn; col++)
            {
                var checkedCookieType = -1;

                for (var row = 0; row < totalRow; row++)
                {
                    var cookieType = game.model.GetCookieType(col, row);
                    if (checkedCookieType != cookieType)
                    {
                        if (checkList.Count >= 3)
                        {
                            _groupedIndex.AddRange(checkList);
                        }

                        checkedCookieType = cookieType;
                        checkList.Clear();
                    }

                    if (cookieType == -1)
                    {
                        continue;
                    }

                    checkList.Add(col + row * totalColumn);
                }

                if (checkList.Count >= 3)
                {
                    _groupedIndex.AddRange(checkList);
                }

                checkList.Clear();
            }
        }
    }
}