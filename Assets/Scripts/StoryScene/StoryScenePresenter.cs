using System;
using System.Collections;
using System.Collections.Generic;
using Game.Logic.State;
using UniRx;
using UnityEngine;
using Utilities.Audio;
using Zenject;

public class StoryScenePresenter : MonoBehaviour
{
    [SerializeField] private StorySceneView view;
    public readonly StorySceneModel model = new StorySceneModel();
    
    [Inject]
    private IAudioManager _audioManager;
    
    void Start()
    {
        BindViewCallbacks();
        BindModelSubscription();
        
        _audioManager.PlayBGM("Story");
        
        model.Init();
    }

    private void BindViewCallbacks()
    {
        view.onEnlargeButtonClicked.AddListener(model.OnClickEnlargeButton);
        view.onSkipButtonClicked.AddListener(model.OnClickSkipButton);
        view.onTextPlayButtonClicked.AddListener(model.OnClickTextPlayButton);
    }
    
    private void BindModelSubscription()
    {
        model.isEnlargeMode.Subscribe(isEnlarge =>
        {
            EnlargeModeToggleShow(!isEnlarge);
        });
        
        model.currentStoryIndex.Subscribe(currentStoryIndex =>
        {
            UpdateStoryData(model.storyDataText, model.storyBackgroundIndex);
        });
    }

    private void EnlargeModeToggleShow(bool show)
    {
        view.textMainBase.SetActive(show);
    }

    private void UpdateStoryData(string storyText, int backgroundIndex)
    {
        view.storyTextView.text = storyText;
    }
}
