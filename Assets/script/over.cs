using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOver : MonoBehaviour
{
    [SerializeField] GameObject gameover_menu;

    [Header("Audio")]
    [SerializeField] private AudioClip gameOverSound;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.ignoreListenerPause = true; // tetap bunyi walau timeScale = 0
    }

    public void TriggerGameOver()
    {
        var rev = FindFirstObjectByType<reverse>();
        if (rev != null) rev.ForceStop();
        gameover_menu.SetActive(true);
        Time.timeScale = 0f;

        if (gameOverSound != null && audioSource != null)
            audioSource.PlayOneShot(gameOverSound);
    }

    public void Retry()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Home()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("level_menu");
    }
}