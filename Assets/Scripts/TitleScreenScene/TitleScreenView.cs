using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleScreenView : MonoBehaviour
{
    [SerializeField] private Button startButton;

    public Button.ButtonClickedEvent onStartButtonClicked => startButton.onClick;
}
