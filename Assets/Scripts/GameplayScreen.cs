using TMPro;
using UnityEngine;

public class GameplayScreen : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _highScoreText;

    private void OnEnable()
    {
        EventManager.OnUpdateScoreEvent += UpdateScoreEvent;
        EventManager.OnUpdateHighScoreEvent += HighScoreEvent;
    }

    private void OnDisable()
    {
        EventManager.OnUpdateScoreEvent -= UpdateScoreEvent;
        EventManager.OnUpdateHighScoreEvent -= HighScoreEvent;
    }

    private void UpdateScoreEvent(int newScore)
    {
        _scoreText.text = newScore.ToString();
        
        // yet to check
        // if (newScore == 0)
        // {
        // }
        // else
        // {
        // }
    }

    private void HighScoreEvent(int newHighScore)
    {
        _highScoreText.text = newHighScore.ToString();
    }
}