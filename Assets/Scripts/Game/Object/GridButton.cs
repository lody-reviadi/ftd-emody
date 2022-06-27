using System.Collections.Generic;
using Game.Data;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Object
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class GridButton : MonoBehaviour
    {
        [SerializeField] private int column;
        [SerializeField] private int row;
        
        [SerializeField] private CookieDataBank cookieDataBank;
        [SerializeField] private SpriteRenderer cookieSprite;

        public readonly UnityEvent<int, int> onGridClicked = new();
        private void OnMouseDown()
        {
            onGridClicked.Invoke(column, row);
        }
        
        public void SetCookieSprites(int cookieIndex)
        {
            if (cookieIndex < 0)
            {
                cookieSprite.gameObject.SetActive(false);
                return;
            }
            
            var newSprite = cookieDataBank.GetSpriteByIndex(cookieIndex);
            
            if (newSprite == null)
            {
                return;
            }

            cookieSprite.sprite = newSprite;
            cookieSprite.gameObject.SetActive(true);
        }
    }
}