using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities.Audio;
using Zenject;

public class TitleScreenPresenter : MonoBehaviour
{
    [SerializeField] private TitleScreenView view;
    [Inject]
    private IAudioManager _audioManager;
    

    public readonly TitleScreenModel _model = new TitleScreenModel();

    void Start()
    {
        BindViewCallbacks();
        _audioManager.Play("Title");
    }

    private void BindViewCallbacks()
    {
        view.onStartButtonClicked.AddListener(_model.OnStartButtonClicked);
    }
}
