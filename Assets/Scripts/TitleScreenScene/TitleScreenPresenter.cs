using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Utilities.Audio;
using Zenject;

public class TitleScreenPresenter : MonoBehaviour
{
    [SerializeField] private TitleScreenView view;
    [Inject]
    private IAudioManager _audioManager;
    

    public readonly TitleScreenModel model = new TitleScreenModel();

    void Start()
    {
        BindViewCallbacks();
        BindModelSubscription();
        _audioManager.PlayBGM("Title");
    }

    private void BindViewCallbacks()
    {
        view.onStartButtonClicked.AddListener(model.OnStartButtonClicked);
        view.onDifficultyButtonClickedEvent.AddListener(model.OnDifficultyButtonClicked);
    }

    private void BindModelSubscription()
    {
        model.isStartButtonClicked.Subscribe((isClicked) =>
        {
            if (isClicked)
            {
                HideStartButton();
                ShowDifficultyButtons();
            }
        });
    }

    private void HideStartButton()
    {
        view.startButtonBase.SetActive(false);
    }

    private void ShowDifficultyButtons()
    {
        view.difficultyButtonsBase.SetActive(true);
    }
}
