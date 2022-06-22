using System.Collections.Generic;
using Game.Notification;
using Game.Object;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game.Logic
{
    public class GameView : MonoBehaviour
    {
        [SerializeField] private DropObject dropObject;
        public DropObject DropObject => dropObject;
        
        [SerializeField] private TMP_Text cookieCounter;
        [SerializeField] private string counterFormat = "x {0}";
        
        [Header("Controls")]
        [SerializeField] private Button changeShapeButton;
        public Button.ButtonClickedEvent OnChangeShapeButtonClicked => changeShapeButton.onClick;
        
        [SerializeField] private Button rotateCWButton;
        public Button.ButtonClickedEvent OnRotateCWButtonClicked => rotateCWButton.onClick;
        
        [SerializeField] private Button rotateCCWButton;
        public Button.ButtonClickedEvent OnRotateCCWButtonClicked => rotateCCWButton.onClick;

        [SerializeField] private Button dropButton;
        public Button.ButtonClickedEvent OnDropButtonClicked => dropButton.onClick;

        [SerializeField] private List<GridButton> gridButtons;
        [HideInInspector]
        public UnityEvent<int, int> onGridClickedEvent = new();

        [SerializeField] private CommonNotification gameStartNotification;
        public UnityEvent OnGameStartNotificationEnd => gameStartNotification.OnShowNotificationEndEvent;
        

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
        
        public void ShowGameStartNotification()
        {
            gameStartNotification.Show();
        }

        public void UpdateCookieCounter(int newCount)
        {
            cookieCounter.text = string.Format(counterFormat, newCount);
        }
    }
}