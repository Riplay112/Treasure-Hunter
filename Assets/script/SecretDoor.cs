using UnityEngine;
using UnityEngine.SceneManagement;

public class SecretDoor : MonoBehaviour
{
    [Header("Scene")]
    public string nextSceneName = "level_2";
    public string playerTag = "Player";

    [Header("UI")]
    public GameObject interactPrompt;

    [Header("Door Reveal")]
    public SpriteRenderer doorSprite;
    public SpriteRenderer platformSprite;

    private bool playerNearby = false;

    void Start()
    {
        if (interactPrompt != null)
            interactPrompt.SetActive(false);

        // Sembunyikan dengan alpha 0
        SetAlpha(doorSprite, 0f);
        SetAlpha(platformSprite, 0f);
    }

    void Update()
    {
        if (playerNearby && Input.GetKeyDown(KeyCode.E))
            SceneManager.LoadScene(nextSceneName);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            playerNearby = true;
            if (interactPrompt != null)
                interactPrompt.SetActive(true);

            SetAlpha(doorSprite, 1f);
            SetAlpha(platformSprite, 1f);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            playerNearby = false;
            if (interactPrompt != null)
                interactPrompt.SetActive(false);

            SetAlpha(doorSprite, 0f);
            SetAlpha(platformSprite, 0f);
        }
    }

    void SetAlpha(SpriteRenderer sr, float alpha)
    {
        if (sr == null) return;
        Color c = sr.color;
        c.a = alpha;
        sr.color = c;
    }
}