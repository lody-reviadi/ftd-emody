using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StorySceneModel
{
    private const string STORY_TEXT_PATH = "story";
    private bool _isEnlargeMode = false;
    private bool _isValueUpdated = false;
    private StoryDatas _storyDatas;

    [System.Serializable]
    private class StoryDatas
    {
        public StoryData[] stories;
    }

    [System.Serializable]

    private class StoryData
    {
        public string text;
        public int background;
    }

    public void Init()
    {
        string storyJson = Resources.Load<TextAsset>(STORY_TEXT_PATH).ToString();
        _storyDatas = JsonUtility.FromJson<StoryDatas>(storyJson);
    }

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

    public void OnClickSkipButton()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
