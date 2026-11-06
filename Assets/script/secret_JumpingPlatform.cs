using UnityEngine;

public class Secret_JumpingPlatform : MonoBehaviour
{
    public string playerTag = "Player";
    public GameObject[] platforms; // drag semua platform kecil ke sini

    void Start()
    {
        foreach (var p in platforms)
            if (p != null) p.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
            foreach (var p in platforms)
                if (p != null) p.SetActive(true);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
            foreach (var p in platforms)
                if (p != null) p.SetActive(false);
    }
}