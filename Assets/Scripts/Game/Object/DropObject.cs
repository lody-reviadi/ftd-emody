using Game.Data;
using Game.Helper;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Game.Object
{
    public class DropData
    {
        public int CenterId { get; }
        public int Side1Id { get; }
        public int Side2Id { get; }

        public PositionType Side1Pos { get; private set; }
        public PositionType Side2Pos { get; private set; }
        
        public int Column { get; private set; }
        public int Row { get; private set; }
        public int Depth { get; private set; }

        public UnityEvent onShapeChanged = new();
        public UnityEvent onRotated = new();
        public UnityEvent onPositionUpdated = new();

        public DropData(int center = 0, int side1 = 0, int side2 = 0, int initialDepth = 10)
        {
            CenterId = center;
            Side1Id = side1;
            Side2Id = side2;

            Column = 2;
            Row = 2;
            Depth = initialDepth;
        }

        public void ChangeShape()
        {
            Side1Pos = Side1Pos.RotateCW();

            if (Side1Pos == Side2Pos)
            {
                Side1Pos = Side1Pos.RotateCW();
            }

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

        public static DropData GetRandomData()
        {
            var cookie1 = Random.Range(0, 4);
            var cookie2 = Random.Range(0, 4);
            var cookie3 = Random.Range(0, 4);

            var randomData = new DropData(cookie1, cookie2, cookie3);

            var randomIndex1 = Random.Range(0, 4);
            var randomIndex2 = (randomIndex1 + Random.Range(1, 4)) % 4;
            
            randomData.Side1Pos = (PositionType)randomIndex1;
            randomData.Side2Pos = (PositionType)randomIndex2;
            
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

        public void Start()
        {
            Bind(DropData.GetRandomData());
        }

        public void Bind(DropData dropData)
        { 
            Unbind();

            _data = dropData;
            
            UpdateSprites();
            UpdateFormation();
            
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
            
            _data.onRotated.RemoveListener(UpdateFormation);
            _data.onShapeChanged.RemoveListener(UpdateFormation);
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
            transform.localPosition = new Vector3(-2 + _data.Column, -2 + _data.Row, -9f);
            transform.localScale = Vector3.one * (1f + _data.Depth * 0.2f);
        }

        private void SetSprite(SpriteRenderer cookieSprite, int id)
        {
            cookieSprite.sprite = cookieDataBank.GetSpriteByIndex(id);
        }
    }
}