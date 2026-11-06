using System.Collections;
using UnityEngine;
using TMPro;

public class reverse : MonoBehaviour
{
    [Header("Reversal Settings")]
    public float minDuration = 2f;
    public float maxDuration = 8f;
    public float tileCooldown = 1f;

    [Header("Warning Flash UI")]
    public TextMeshProUGUI warningText;
    public Color warningColor = new Color(1f, 0.3f, 0.3f, 1f);
    public Color normalColor = Color.white;
    public float flashInterval = 0.15f;

    [Header("UI Feedback")]
    public GameObject reversedUIIndicator;

    [Header("Audio")]
    public AudioClip activateSound;
    public AudioClip deactivateSound;

    private bool _isOnCooldown = false;
    private AudioSource _audio;
    private movement _currentPlayer;

    void Awake()
    {
        _audio = GetComponent<AudioSource>();
        if (_audio == null) _audio = gameObject.AddComponent<AudioSource>();
        if (warningText != null) warningText.gameObject.SetActive(false);
        if (reversedUIIndicator != null) reversedUIIndicator.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (_isOnCooldown) return;
        if (!other.CompareTag("Player")) return;

        var mv = other.GetComponent<movement>();
        if (mv == null) return;

        StartCoroutine(ReverseSequence(mv));
    }

    IEnumerator ReverseSequence(movement mv)
    {
        _isOnCooldown = true;
        _currentPlayer = mv;
        float randomDuration = Random.Range(minDuration, maxDuration);

        mv.isReversed = true;
        PlaySound(activateSound);

        if (warningText != null)
        {
            warningText.gameObject.SetActive(true);
            warningText.text = "REVERSED CONTROLS!";
        }

        if (reversedUIIndicator != null)
            reversedUIIndicator.SetActive(true);

        StartCoroutine(FlashWarning(randomDuration));

        yield return new WaitForSeconds(randomDuration);

        mv.isReversed = false;
        _currentPlayer = null;
        PlaySound(deactivateSound);

        if (warningText != null) warningText.gameObject.SetActive(false);
        if (reversedUIIndicator != null) reversedUIIndicator.SetActive(false);

        yield return new WaitForSeconds(tileCooldown);
        _isOnCooldown = false;
    }

    IEnumerator FlashWarning(float duration)
    {
        float elapsed = 0f;
        bool toggle = false;

        while (elapsed < duration)
        {
            if (warningText != null)
                warningText.color = toggle ? warningColor : normalColor;
            toggle = !toggle;
            elapsed += flashInterval;
            yield return new WaitForSeconds(flashInterval);
        }
    }

    // Panggil ini saat Game Over atau Pause
    public void ForceStop()
    {
        StopAllCoroutines();
        if (warningText != null)
        {
            warningText.gameObject.SetActive(false);
            warningText.text = "";
            warningText.enabled = false; // ← paksa disable renderer-nya
        }
        if (reversedUIIndicator != null) reversedUIIndicator.SetActive(false);
        if (_currentPlayer != null)
        {
            _currentPlayer.isReversed = false;
            _currentPlayer = null;
        }
        _isOnCooldown = false;
    }

    void PlaySound(AudioClip clip)
    {
        if (clip != null && _audio != null)
            _audio.PlayOneShot(clip);
    }
}