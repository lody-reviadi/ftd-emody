using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StorySceneModel
{
    private const string STORY_TEXT_PATH = "story";
    private readonly BoolReactiveProperty _isEnlargeMode = new();
    public IReadOnlyReactiveProperty<bool> isEnlargeMode => _isEnlargeMode;
    private bool _isValueUpdated = false;
    private int _currentStoryIndex = 0;
    private StoryDatas _storyDatas;

    public string storyDataText
    {
        get
        {
            return _storyDatas.stories[_currentStoryIndex].text;
        }
    }

    public int storyBackgroundIndex
    {
        get
        {
            return _storyDatas.stories[_currentStoryIndex].background;
        }
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
        _isEnlargeMode.Value = false;
        _isValueUpdated = true;
    }
    
    public void OnClickEnlargeButton()
    {
        _isEnlargeMode.Value = !_isEnlargeMode.Value;
    }

    public void OnClickSkipButton()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void OnClickTextPlayButton()
    {
        _currentStoryIndex = Mathf.Min(_currentStoryIndex + 1, _storyDatas.stories.Length - 1);
        _isValueUpdated = true;
    }
}
