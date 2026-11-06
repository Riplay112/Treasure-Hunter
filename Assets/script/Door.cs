using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorInteract : MonoBehaviour
{
    [Tooltip("Nama scene level selanjutnya")]
    public string nextSceneName = "level_2";

    [Tooltip("Tag player")]
    public string playerTag = "Player";

    [Tooltip("GameObject sprite 'Tekan E' (E_Interact)")]
    public GameObject interactPrompt;

    private bool playerNearby = false;

    private void Start()
    {
        // Sembunyikan prompt saat awal
        if (interactPrompt != null)
            interactPrompt.SetActive(false);
    }

    private void Update()
    {
        if (playerNearby && Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            playerNearby = true;
            if (interactPrompt != null)
                interactPrompt.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            playerNearby = false;
            if (interactPrompt != null)
                interactPrompt.SetActive(false);
        }
    }
}