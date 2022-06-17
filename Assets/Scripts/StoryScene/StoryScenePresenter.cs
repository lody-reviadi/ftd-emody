using System;
using System.Collections;
using System.Collections.Generic;
using Game.Logic.State;
using UnityEngine;

public class StoryScenePresenter : MonoBehaviour
{
    [SerializeField] private StorySceneView view;
    public readonly StorySceneModel model = new();
    
    void Start()
    {
        BindViewCallbacks();
    }

    private void BindViewCallbacks()
    {
        view.onEnlargeButtonClicked.AddListener(model.OnClickEnlargeButton);
    }

    private void EnlargeModeToogleShow(bool show)
    {
        view.textMainBase.SetActive(show);
    }

    private void FixedUpdate()
    {
        if (model.isValueUpdated)
        {
            EnlargeModeToogleShow(!model.isEnlargeMode);
            model.isValueUpdated = false;
        }
    }
}
