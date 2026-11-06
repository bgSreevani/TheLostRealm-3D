using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverPanel;

    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI finalOrbText;

    public DistanceManager distanceManager;
    public OrbCollector orbCollector;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip gameOverSound;

    private bool isGameOver = false;

    void Start()
    {
        gameOverPanel.SetActive(false);
    }

    public void GameOver()
    {
        if (isGameOver)
            return;

        isGameOver = true;

        if (audioSource != null && gameOverSound != null)
        {
            audioSource.PlayOneShot(gameOverSound);
        }

        gameOverPanel.SetActive(true);

        int finalDistance = 0;

        if (distanceManager != null)
        {
            finalDistance = distanceManager.GetDistance();
        }

        int finalOrbs = 0;

        if (orbCollector != null)
        {
            finalOrbs = orbCollector.orbCount;
        }

        finalScoreText.text =
            "Distance: " + finalDistance + " m";

        finalOrbText.text =
            "Orbs Collected: " + finalOrbs;

        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }
}