using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class StorySceneView : MonoBehaviour
{
    [Header("Controls")]
    [SerializeField] private Button enlargeButton;
    public Button.ButtonClickedEvent onEnlargeButtonClicked => enlargeButton.onClick;
}
