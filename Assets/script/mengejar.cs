using System.Collections;
using UnityEngine;

public class SequentialSpike : MonoBehaviour
{
    [Header("Spike Settings")]
    [Tooltip("Seberapa tinggi spike naik (unit)")]
    public float riseHeight = 2f;
    [Tooltip("Jeda antar spike naik (detik)")]
    public float delayBetweenSpikes = 0.1f;
    [Tooltip("Delay sebelum sequence mulai (detik)")]
    public float startDelay = 0f;
    [Tooltip("Berapa lama spike diam di atas sebelum turun (detik)")]
    public float stayDuration = 0.5f;

    private Transform[] _spikes;
    private float[] _startYPositions;
    private bool _isActive = false;

    void Start()
    {
        _spikes = new Transform[transform.childCount];
        _startYPositions = new float[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            _spikes[i] = transform.GetChild(i);
            _startYPositions[i] = _spikes[i].position.y;
        }

        // Sort kanan ke kiri (bubble sort)
        for (int i = 0; i < _spikes.Length - 1; i++)
        {
            for (int j = i + 1; j < _spikes.Length; j++)
            {
                if (_spikes[i].position.x < _spikes[j].position.x)
                {
                    var tempSpike = _spikes[i];
                    _spikes[i] = _spikes[j];
                    _spikes[j] = tempSpike;

                    var tempY = _startYPositions[i];
                    _startYPositions[i] = _startYPositions[j];
                    _startYPositions[j] = tempY;
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        if (_isActive) return; 
        _isActive = true;
        StartCoroutine(RiseSequence());
    }

    IEnumerator RiseSequence()
    {
        if (startDelay > 0f)
            yield return new WaitForSeconds(startDelay);

        // Naik satu per satu kanan ke kiri
        for (int i = 0; i < _spikes.Length; i++)
        {
            var spikeScript = _spikes[i].GetComponent<SpikeUP>();
            if (spikeScript != null)
                spikeScript.MulaiNaik(_startYPositions[i] + riseHeight);

            yield return new WaitForSeconds(delayBetweenSpikes);
        }

        // Diam di atas
        yield return new WaitForSeconds(stayDuration);

        // Turun semua sekaligus
        for (int i = 0; i < _spikes.Length; i++)
        {
            var spikeScript = _spikes[i].GetComponent<SpikeUP>();
            if (spikeScript != null)
                spikeScript.MulaiTurun(_startYPositions[i]);
        }
    }
}