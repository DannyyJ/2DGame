using UnityEngine;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{
    public Button restartButton;
    public Button menuButton;
    public Button easyButton;
    public Button mediumButton;
    public Button hardButton;

    void Start()
    {
        if (restartButton != null)
            restartButton.onClick.AddListener(() => GameManager.Instance.RestartGame());

        if (menuButton != null)
            menuButton.onClick.AddListener(() => GameManager.Instance.ReturnToMenu());

        if (easyButton != null)
            easyButton.onClick.AddListener(() => GameManager.Instance.SetDifficultyAndStartGame(0));

        if (mediumButton != null)
            mediumButton.onClick.AddListener(() => GameManager.Instance.SetDifficultyAndStartGame(1));

        if (hardButton != null)
            hardButton.onClick.AddListener(() => GameManager.Instance.SetDifficultyAndStartGame(2));
    }
}
