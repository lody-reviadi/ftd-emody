using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorySceneModel
{
    private bool _isEnlargeMode = false;
    private bool _isValueUpdated = false;

    public bool isValueUpdated
    {
        get
        {
            return _isValueUpdated;
        }
        set
        {
            _isValueUpdated = value;
        }
    }

    public bool isEnlargeMode
    {
        get
        {
            return _isEnlargeMode;
        }
    }
    
    public void OnClickEnlargeButton()
    {
        _isEnlargeMode = !_isEnlargeMode;
        _isValueUpdated = true;
    }
}
