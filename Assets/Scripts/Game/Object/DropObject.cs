using Game.Data;
using Game.Helper;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Game.Object
{
    public class DropData
    {
        public int CenterId { get; private set; }
        public int Side1Id { get; private set; }
        public int Side2Id { get; private set; }

        public PositionType Side1Pos { get; private set; }
        public PositionType Side2Pos { get; private set; }
        
        public int Column { get; private set; }
        public int Row { get; private set; }

        public int Side1Column => Side1Pos switch
        {
            PositionType.Left => Column - 1,
            PositionType.Right => Column + 1,
            _ => Column
        };

        public int Side1Row => Side1Pos switch
        {
            PositionType.Bottom => Row - 1,
            PositionType.Top => Row + 1,
            _ => Row
        };
        
        public int Side2Column => Side2Pos switch
        {
            PositionType.Left => Column - 1,
            PositionType.Right => Column + 1,
            _ => Column
        };

        public int Side2Row => Side2Pos switch
        {
            PositionType.Bottom => Row - 1,
            PositionType.Top => Row + 1,
            _ => Row
        };
        
        public int Depth { get; private set; }

        public readonly UnityEvent onInitialize = new();
        public readonly UnityEvent onShapeChanged = new();
        public readonly UnityEvent onRotated = new();
        public readonly UnityEvent onPositionUpdated = new();

        public DropData(int center = 0, int side1 = 0, int side2 = 0, PositionType side1Pos = PositionType.Top,
            PositionType side2Pos = PositionType.Bottom, int initialDepth = 10)
        {
            Initialize(center, side1, side2, side1Pos, side2Pos, initialDepth);
        }

        public void Initialize(int center = 0, int side1 = 0, int side2 = 0, PositionType side1Pos = PositionType.Top,
            PositionType side2Pos = PositionType.Bottom, int initialDepth = 10)
        {
            CenterId = center;
            Side1Id = side1;
            Side2Id = side2;

            Side1Pos = side1Pos;
            Side2Pos = side2Pos;

            Column = 2;
            Row = 2;
            Depth = initialDepth;

            onInitialize.Invoke();
        }

        public void ChangeShape()
        {
            Side1Pos = Side1Pos.RotateCW();

            if (Side1Pos == Side2Pos)
            {
                Side1Pos = Side1Pos.RotateCW();
            }
            
            UpdatePosition(Column, Row);

            onShapeChanged.Invoke();
        }

        public void RotateCW()
        {
            Side1Pos = Side1Pos.RotateCW();
            Side2Pos = Side2Pos.RotateCW();
            
            UpdatePosition(Column, Row);
            
            onRotated.Invoke();
        }
        
        public void RotateCCW()
        {
            Side1Pos = Side1Pos.RotateCCW();
            Side2Pos = Side2Pos.RotateCCW();

            UpdatePosition(Column, Row);
            
            onRotated.Invoke();
        }

        public void UpdatePosition(int col, int row)
        {
            Column = col switch
            {
                4 when (Side1Pos == PositionType.Right || Side2Pos == PositionType.Right) => col - 1,
                0 when (Side1Pos == PositionType.Left || Side2Pos == PositionType.Left) => col + 1,
                _ => col
            };

            Row = row switch
            {
                4 when (Side1Pos == PositionType.Top || Side2Pos == PositionType.Top) => (row - 1),
                0 when (Side1Pos == PositionType.Bottom || Side2Pos == PositionType.Bottom) => (row + 1),
                _ => row
            };
            
            onPositionUpdated.Invoke();
        }

        public void DropDown()
        {
            if (Depth <= 0)
            {
                return;
            }
            
            Depth--;
            onPositionUpdated.Invoke();
        }

        public void InstantDrop()
        {
            Depth = 0;
            onPositionUpdated.Invoke();
        }

        public static DropData GetRandomData()
        {
            var cookie1 = Random.Range(0, 4);
            var cookie2 = Random.Range(0, 4);
            var cookie3 = Random.Range(0, 4);
            
            var randomIndex1 = Random.Range(0, 4);
            var randomIndex2 = (randomIndex1 + Random.Range(1, 4)) % 4;
            
            var side1Pos = (PositionType)randomIndex1;
            var side2Pos = (PositionType)randomIndex2;

            var randomData = new DropData(cookie1, cookie2, cookie3, side1Pos, side2Pos, 0);
            
            return randomData;
        }
    }
    public class DropObject : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer centerSprite;
        
        [SerializeField] private SpriteRenderer sideSprite1;
        [SerializeField] private Transform side1Transform;
        
        [SerializeField] private SpriteRenderer sideSprite2;
        [SerializeField] private Transform side2Transform;

        [SerializeField] private float distanceFromCenter = 1;

        [SerializeField] private CookieDataBank cookieDataBank;

        private DropData _data;

        public void Bind(DropData dropData)
        { 
            Unbind();

            _data = dropData;
            
            _data.onInitialize.AddListener(Initialize);
            _data.onRotated.AddListener(UpdateFormation);
            _data.onShapeChanged.AddListener(UpdateFormation);
            _data.onPositionUpdated.AddListener(UpdatePosition);
        }

        private void Unbind()
        {
            if (_data == null)
            {
                return;
            }
            
            _data.onInitialize.RemoveListener(Initialize);
            _data.onRotated.RemoveListener(UpdateFormation);
            _data.onShapeChanged.RemoveListener(UpdateFormation);
            _data.onPositionUpdated.RemoveListener(UpdatePosition);
        }

        private void Initialize()
        {
            UpdateSprites();
            UpdateFormation();
            UpdatePosition();
        }

        private void UpdateSprites()
        {
            SetSprite(centerSprite, _data.CenterId);
            SetSprite(sideSprite1, _data.Side1Id);
            SetSprite(sideSprite2, _data.Side2Id);
        }

        private void UpdateFormation()
        {
            side1Transform.localPosition = _data.Side1Pos.GetVector2Position() * distanceFromCenter;
            side2Transform.localPosition = _data.Side2Pos.GetVector2Position() * distanceFromCenter;
        }

        private void UpdatePosition()
        {
            transform.position = new Vector3(-2 + _data.Column, -2 + _data.Row, -9f);
            transform.localScale = Vector3.one * (1f + _data.Depth * 0.05f);
            SetAlpha(1f - _data.Depth * 0.05f);
        }

        private void SetAlpha(float alpha)
        {
            var newColor = new Color(1f, 1f, 1, alpha);
            centerSprite.color = newColor;
            sideSprite1.color = newColor;
            sideSprite2.color = newColor;
        }

        private void SetSprite(SpriteRenderer cookieSprite, int id)
        {
            cookieSprite.sprite = cookieDataBank.GetSpriteByIndex(id);
        }
    }
}