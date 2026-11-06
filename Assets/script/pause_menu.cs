using UnityEngine;
using UnityEngine.SceneManagement;

public class pause_menu : MonoBehaviour
{
    public GameObject PauseMenuUI;
    public void pause()
    {
        var rev = FindFirstObjectByType<reverse>();
        if (rev != null) rev.ForceStop();

        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        foreach (var btn in PauseMenuUI.GetComponentsInChildren<ButtonAnimator>())
        {
            btn.ResetScale();
        }
    }

    public void resume()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
    }

    public void back()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("level_menu");
    }

    public void retry()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
