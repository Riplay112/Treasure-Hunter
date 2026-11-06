using System.Collections;
using UnityEngine;
public class CrumblingPlatform : MonoBehaviour
{
    [Header("Pengaturan Waktu")]
    [Tooltip("Durasi guncangan sebelum jatuh")]
    public float shakeDuration = 0.3f;
    [Tooltip("Jeda setelah shake sebelum mulai jatuh")]
    public float delayBeforeFall = 0.1f;
    [Tooltip("Kecepatan platform turun ke bawah")]
    public float fallSpeed = 5f;
    [Tooltip("Seberapa jauh platform turun sebelum hilang")]
    public float fallDistance = 10f;
    [Header("Pengaturan Shake")]
    public float shakeMagnitude = 0.05f;
    [Header("Respawn")]
    public bool respawn = true;
    public float respawnDelay = 3f;
    [Header("Tag Player")]
    public string playerTag = "Player";

    [Header("Audio")]
    [SerializeField] private AudioClip shakeSound;
    private AudioSource audioSource;

    // --- Private ---
    private Vector3 originalPosition;
    private bool isFalling = false;

    void Start()
    {
        originalPosition = transform.position;
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isFalling) return;
        if (!collision.gameObject.CompareTag(playerTag)) return;
        foreach (ContactPoint2D contact in collision.contacts)
        {
            if (contact.normal.y < -0.5f)
            {
                StartCoroutine(CrumbleRoutine());
                break;
            }
        }
    }

    private IEnumerator CrumbleRoutine()
    {
        isFalling = true;

        // === FASE 1: Shake ===
        if (shakeSound != null)
            audioSource.PlayOneShot(shakeSound);
        float elapsed = 0f;
        Vector3 startPos = transform.position;
        while (elapsed < shakeDuration)
        {
            float ox = Random.Range(-shakeMagnitude, shakeMagnitude);
            float oy = Random.Range(-shakeMagnitude, shakeMagnitude);
            transform.position = startPos + new Vector3(ox, oy, 0f);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = startPos;

        // === FASE 2: Delay ===
        yield return new WaitForSeconds(delayBeforeFall);

        // === FASE 3: Jatuh ===
        float fallen = 0f;
        while (fallen < fallDistance)
        {
            float step = fallSpeed * Time.deltaTime;
            transform.position += Vector3.down * step;
            fallen += step;
            yield return null;
        }

        // === FASE 4: Sembunyikan platform ===
        gameObject.SetActive(false);
        if (respawn)
        {
            yield return new WaitForSeconds(respawnDelay);
            ResetPlatform();
        }
    }

    private void ResetPlatform()
    {
        transform.position = originalPosition;
        isFalling = false;
        gameObject.SetActive(true);
    }
}