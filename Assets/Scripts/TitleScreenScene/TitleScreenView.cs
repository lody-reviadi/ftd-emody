using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TitleScreenView : MonoBehaviour
{
    [SerializeField] public GameObject startButtonBase;
    [SerializeField] public GameObject difficultyButtonsBase;
    [SerializeField] private Button _startButton;
    [SerializeField] private Button[] _difficultyButtons;

    public Button.ButtonClickedEvent onStartButtonClicked => _startButton.onClick;
    public UnityEvent<int> onDifficultyButtonClickedEvent = new();
    
    private void Awake()
    {
        Button difficultyButton = _difficultyButtons[0];
        difficultyButton.onClick.AddListener(() =>
        {
            onDifficultyButtonClickedEvent.Invoke(0);
        });
        
        Button difficultyButton2 = _difficultyButtons[1];
        difficultyButton2.onClick.AddListener(() =>
        {
            onDifficultyButtonClickedEvent.Invoke(1);
        });
        
        Button difficultyButton3 = _difficultyButtons[2];
        difficultyButton3.onClick.AddListener(() =>
        {
            onDifficultyButtonClickedEvent.Invoke(2);
        });
    }
}
