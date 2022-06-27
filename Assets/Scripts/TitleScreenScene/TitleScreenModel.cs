using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenModel
{
    private readonly BoolReactiveProperty _isStartButtonClicked = new(false);
    public IReadOnlyReactiveProperty<bool> isStartButtonClicked => _isStartButtonClicked;

    private class DifficultyData
    {
        public int indexId;
        public int varianceCount;

        public DifficultyData(int indexId, int varianceCount)
        {
            this.indexId = indexId;
            this.varianceCount = varianceCount;
        }
    }

    private DifficultyData[] _difficultyDatas = new[]
    {
        // NORMAL
        new DifficultyData(0, 4),
        // HARD
        new DifficultyData(1, 3),
        // THIS IS FIRE
        new DifficultyData(2, 2)
    };
    
    public void OnStartButtonClicked()
    {
        _isStartButtonClicked.Value = true;
    }

    public void OnDifficultyButtonClicked(int indexId)
    {
        for (int i = 0; i < _difficultyDatas.Length; i++)
        {
            if (_difficultyDatas[i].indexId == indexId)
            {
                int variance = _difficultyDatas[i].varianceCount;
                PlayerPrefs.SetInt("Variance", variance);
                SceneManager.LoadScene("StoryScene");
            }
        }
    }
}
