using UnityEngine;
public class SpikeUP : MonoBehaviour
{
    public float kecepatanNaik = 8f;

    [Header("Audio")]
    [SerializeField] private AudioClip suaraNaik;
    private AudioSource audioSource;

    private bool sedangGerak = false;
    private float targetY;
    private System.Action onSelesai;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void MulaiNaik(float tujuanY, System.Action callback = null)
    {
        targetY = tujuanY;
        onSelesai = callback;
        sedangGerak = true;

        // Putar suara saat spike naik
        if (suaraNaik != null && audioSource != null)
            audioSource.PlayOneShot(suaraNaik);
    }

    public void MulaiTurun(float tujuanY, System.Action callback = null)
    {
        targetY = tujuanY;
        onSelesai = callback;
        sedangGerak = true;
    }

    void Update()
    {
        if (!sedangGerak) return;
        transform.position = Vector3.MoveTowards(
            transform.position,
            new Vector3(transform.position.x, targetY, transform.position.z),
            kecepatanNaik * Time.deltaTime
        );
        if (Mathf.Abs(transform.position.y - targetY) < 0.05f)
        {
            transform.position = new Vector3(transform.position.x, targetY, transform.position.z);
            sedangGerak = false;
            onSelesai?.Invoke();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameOver gameOver = other.GetComponent<GameOver>();
            if (gameOver != null)
                gameOver.TriggerGameOver();
        }
    }
}