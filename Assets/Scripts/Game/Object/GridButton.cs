using UnityEngine;
using UnityEngine.Events;

namespace Game.Object
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class GridButton : MonoBehaviour
    {
        [SerializeField] private int column;
        [SerializeField] private int row;

        public readonly UnityEvent<int, int> onGridClicked = new();
        private void OnMouseDown()
        {
            onGridClicked.Invoke(column, row);
        }
    }
}