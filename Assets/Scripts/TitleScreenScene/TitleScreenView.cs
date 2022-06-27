using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleScreenView : MonoBehaviour
{
    [SerializeField] public GameObject startButtonBase;
    [SerializeField] public GameObject difficultyButtonsBase;
    [SerializeField] private Button startButton;

    public Button.ButtonClickedEvent onStartButtonClicked => startButton.onClick;
}
