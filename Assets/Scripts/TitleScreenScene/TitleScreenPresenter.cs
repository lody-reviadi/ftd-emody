using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenPresenter : MonoBehaviour
{
    [SerializeField] private TitleScreenView view;

    public readonly TitleScreenModel _model = new TitleScreenModel();

    void Start()
    {
        BindViewCallbacks();
    }

    private void BindViewCallbacks()
    {
        view.onStartButtonClicked.AddListener(_model.OnStartButtonClicked);
    }
}
