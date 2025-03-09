using UnityEngine;

public class ScoreHandler : MonoBehaviour
{
    public int CurrentScore { get; private set; }
    private int _highScore;

    private void Start()
    {
        _highScore = PlayerPrefs.GetInt("HighScore", 0);
        EventManager.InvokeUpdateHighScore(_highScore);
    }

    private void OnEnable()
    {
        EventManager.OnFruitMergeEvent += FruitMergeEvent;
    }

    private void OnDisable()
    {
        EventManager.OnFruitMergeEvent -= FruitMergeEvent;
    }

    private void FruitMergeEvent(Fruit fruit1, Fruit fruit2)
    {
        var score = GameManager.Instance.FruitDataAsset.GetFruitDetailByType(fruit1.FruitType).ScoreForMerge;
        CurrentScore += score;
        EventManager.InvokeUpdateScore(CurrentScore);

        if(CurrentScore > _highScore)
        {
            EventManager.InvokeUpdateHighScore(CurrentScore);
            PlayerPrefs.SetInt("HighScore", CurrentScore);
        }
    }

    private void ResetScore()
    {
        CurrentScore = 0;
        EventManager.InvokeUpdateScore(CurrentScore);
    }
}