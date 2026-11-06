using UnityEngine;

/// <summary>
/// Pasang script ini ke GameObject E_Interact.
/// Otomatis animasi naik turun.
/// </summary>
public class E : MonoBehaviour
{
    [Tooltip("Seberapa tinggi naik turunnya")]
    public float amplitude = 0.2f;

    [Tooltip("Seberapa cepat naik turunnya")]
    public float frequency = 2f;

    private Vector3 startPosition;

    private void OnEnable()
    {
        // Reset posisi awal setiap kali muncul
        startPosition = transform.localPosition;
    }

    private void Update()
    {
        float newY = startPosition.y + Mathf.Sin(Time.time * frequency) * amplitude;
        transform.localPosition = new Vector3(startPosition.x, newY, startPosition.z);
    }
}