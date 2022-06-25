using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class StorySceneView : MonoBehaviour
{
    [SerializeField] public GameObject textMainBase;
    [SerializeField] public Text storyTextView;
    [Header("Controls")]
    [SerializeField] private Button enlargeButton;
    [SerializeField] private Button skipButton;
    [SerializeField] private Button textPlayButton;

    public Button.ButtonClickedEvent onEnlargeButtonClicked => enlargeButton.onClick;
    public Button.ButtonClickedEvent onSkipButtonClicked => skipButton.onClick;
    public Button.ButtonClickedEvent onTextPlayButtonClicked => textPlayButton.onClick;
}
