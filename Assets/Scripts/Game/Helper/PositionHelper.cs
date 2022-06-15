using UnityEngine;

namespace Game.Helper
{
    public enum PositionType
    {
        Top = 0,
        Right = 1,
        Bottom = 2,
        Left = 3
    }
    public static class PositionHelper
    {
        public static Vector2 GetVector2Position(this PositionType position)
        {
            var pos = position switch
            {
                PositionType.Top => Vector2.up,
                PositionType.Right => Vector2.right,
                PositionType.Bottom => Vector2.down,
                PositionType.Left => Vector2.left,
                _ => Vector2.zero
            };

            return pos;
        }

        public static PositionType RotateCW(this PositionType position)
        {
            var i = (int)position;
            i = (i + 1) % 4;
            
            return (PositionType)i;
        }
        
        public static PositionType RotateCCW(this PositionType position)
        {
            var i = (int)position;
            i = (i + 3) % 4;
            
            return (PositionType)i;
        }
    }
}