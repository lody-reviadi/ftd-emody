using System;
using System.Collections.Generic;
using Game.Data;
using Game.Object;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game.Logic
{
    public class GameView : MonoBehaviour
    {
        [SerializeField] private DropObject dropObject;
        
        public DropObject DropObject => dropObject;

        [Header("Controls")]
        [SerializeField] private Button changeShapeButton;
        public Button.ButtonClickedEvent OnChangeShapeButtonClicked => changeShapeButton.onClick;
        
        [SerializeField] private Button rotateCWButton;
        public Button.ButtonClickedEvent OnRotateCWButtonClicked => rotateCWButton.onClick;
        
        [SerializeField] private Button rotateCCWButton;
        public Button.ButtonClickedEvent OnRotateCCWButtonClicked => rotateCCWButton.onClick;

        [SerializeField] private List<GridButton> gridButtons;
        [HideInInspector]
        public UnityEvent<int, int> onGridClickedEvent = new();

        private void Awake()
        {
            foreach (var gridButton in gridButtons)
            {
                gridButton.onGridClicked.AddListener(OnGridClicked);
            }
        }

        private void OnGridClicked(int col, int row)
        {
            onGridClickedEvent.Invoke(col, row);
        }
        
        public void SetCookieSprites(int index, int cookieIndex)
        {
            if (index < 0 || index > gridButtons.Count)
            {
                return;
            }

            gridButtons[index].SetCookieSprites(cookieIndex);
        }
    }
}