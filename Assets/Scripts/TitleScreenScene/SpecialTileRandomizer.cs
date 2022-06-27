using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialTileRandomizer : MonoBehaviour
{
    [SerializeField] private GameObject[] _specialTiles;
    [SerializeField] private Vector2 _offsetPosition = new Vector2(64f, 64f);
    [SerializeField] private Vector2 _tileSize = new Vector2(128f, 128f);
    [SerializeField] private int _maxRandomTilePostX;
    [SerializeField] private int _maxRandomTilePostY;
    
    
    public void RandomizeSpecialTilePosition()
    {
        for (int i = 0; i < _specialTiles.Length; i++)
        {
            GameObject specialTile = _specialTiles[i];
            int randomX = (int) Random.Range(_maxRandomTilePostX / -2f, _maxRandomTilePostX / 2f);
            int randomY = (int) Random.Range(_maxRandomTilePostY / -2f, _maxRandomTilePostY / 2f);
            float finalX = randomX * _tileSize.x + _offsetPosition.x;
            float finalY = randomY * _tileSize.y + _offsetPosition.y;
            specialTile.transform.localPosition = new Vector3(
                finalX,
                finalY,
                0f
            );
        }
        
    }
}
