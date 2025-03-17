using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScreen : MonoBehaviour
{
    [SerializeField] private Button playButton;

    private void OnEnable()
    {
        playButton.onClick.AddListener(OnPlayButtonClick);
    }

    private void OnDisable()
    {
        playButton.onClick.RemoveListener(OnPlayButtonClick);
    }

    private void OnPlayButtonClick()
    {
        SceneManager.LoadScene("GameScene");
    }
}