using System.Collections.Generic;
using UnityEngine;

namespace Game.Data
{
    [CreateAssetMenu(fileName = "Cookie Data Bank", menuName = "Game Data Bank/1. Cookie Data Bank", order = 1)]
    public class CookieDataBank : ScriptableObject
    {
        [SerializeField] private List<Sprite> cookieTextures;

        public Sprite GetSpriteByIndex(int index)
        {
            return cookieTextures[index % cookieTextures.Count];
        }
    }    
}

