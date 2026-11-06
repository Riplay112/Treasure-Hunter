using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class FakeDoor : MonoBehaviour
{
    [Header("Scene")]
    public string nextSceneName = "level_2";
    public string playerTag = "Player";

    [Header("UI")]
    public GameObject interactPrompt;

    [Header("Spike")]
    public Transform spikeObject;    // drag Spike_drop ke sini
    public float targetY = 0f;       // posisi Y tujuan (lantai)
    public float kecepatanTurun = 6f;

    private bool playerNearby = false;
    private bool sudahTriger = false;

    void Start()
    {
        if (interactPrompt != null)
            interactPrompt.SetActive(false);
    }

    void Update()
    {
        if (playerNearby && !sudahTriger && Input.GetKeyDown(KeyCode.E))
        {
            sudahTriger = true;
            if (interactPrompt != null)
                interactPrompt.SetActive(false);

            StartCoroutine(TurunkanSpike());
        }
    }

    IEnumerator TurunkanSpike()
    {
        // Turunkan spike sampai targetY
        while (spikeObject.position.y > targetY)
        {
            spikeObject.position = Vector3.MoveTowards(
                spikeObject.position,
                new Vector3(spikeObject.position.x, targetY, spikeObject.position.z),
                kecepatanTurun * Time.deltaTime
            );
            yield return null;
        }

        // Setelah sampai, pindah scene
        SceneManager.LoadScene(nextSceneName);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            playerNearby = true;
            if (interactPrompt != null)
                interactPrompt.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            playerNearby = false;
            if (interactPrompt != null)
                interactPrompt.SetActive(false);
        }
    }
}