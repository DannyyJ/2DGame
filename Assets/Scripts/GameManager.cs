using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public enum Difficulty { Easy, Medium, Hard }
    public Difficulty currentDifficulty = Difficulty.Easy;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            if (SceneManager.GetActiveScene().buildIndex != 0)
            {
                SceneManager.LoadScene(0);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RestartGame()
    {
        Debug.Log("Den ska restarta");
        SceneManager.LoadScene(1);
    }

    public void ReturnToMenu()
    {
        Debug.Log("Den ska gå till menyn");
        SceneManager.LoadScene(0);
    }

    public void SetDifficultyAndStartGame(int difficultyIndex)
    {
        Debug.Log("Knapp trycktes");
        currentDifficulty = (Difficulty)difficultyIndex;
        SceneManager.LoadScene(1);
    }
}
