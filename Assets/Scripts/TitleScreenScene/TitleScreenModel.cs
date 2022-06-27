using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenModel
{
    private readonly BoolReactiveProperty _isStartButtonClicked = new(false);
    public IReadOnlyReactiveProperty<bool> isStartButtonClicked => _isStartButtonClicked;
    
    public void OnStartButtonClicked()
    {
        _isStartButtonClicked.Value = true;
    }
}
