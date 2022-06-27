using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StorySceneModel
{
    private const string STORY_TEXT_PATH = "story";
    private readonly BoolReactiveProperty _isEnlargeMode = new(false);
    public IReadOnlyReactiveProperty<bool> isEnlargeMode => _isEnlargeMode;
    
    private readonly IntReactiveProperty _currentStoryIndex = new(-1);
    public IReadOnlyReactiveProperty<int> currentStoryIndex => _currentStoryIndex;
    private StoryDatas _storyDatas;

    public string storyDataText
    {
        get
        {
            return _currentStoryIndex.Value > -1 ? _storyDatas.stories[_currentStoryIndex.Value].text : "";
        }
    }

    public int storyBackgroundIndex
    {
        get
        {
            return _currentStoryIndex.Value > -1 ? _storyDatas.stories[_currentStoryIndex.Value].background : 0;
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
        _currentStoryIndex.Value = 0;
    }
    
    public void OnClickEnlargeButton()
    {
        _isEnlargeMode.Value = !_isEnlargeMode.Value;
    }

    public void OnClickSkipButton()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void OnClickTextPlayButton()
    {
        if (_currentStoryIndex.Value == _storyDatas.stories.Length - 1)
        {
            SceneManager.LoadScene("GameScene");
        }
        _currentStoryIndex.Value = Mathf.Min(
            _currentStoryIndex.Value + 1, _storyDatas.stories.Length - 1
            );
    }
}
