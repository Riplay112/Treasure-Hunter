using UnityEngine;
public class SpikePopUp : MonoBehaviour
{
    [Header("Pengaturan")]
    public float deteksiRadius = 3f;
    public float kecepatanNaik = 5f;
    public float kecepatanTurun = 2f;
    public float offsetTersembunyi = -1f;
    public float offsetMuncul = 0.5f;
    [HideInInspector] public float delayMuncul = 0f;

    [Header("Audio")]
    [SerializeField] private AudioClip suaraMuncul;
    private AudioSource audioSource;

    private Transform pemain;
    private bool sedangNaik = false;
    private bool sudahAktif = false;
    private Vector3 posisiSembunyi;
    private Vector3 posisiMuncul;

    void Start()
    {
        pemain = GameObject.Find("karakter").transform;
        audioSource = GetComponent<AudioSource>();
        Vector3 posisiAwal = transform.position;
        posisiSembunyi = new Vector3(posisiAwal.x, posisiAwal.y + offsetTersembunyi, posisiAwal.z);
        posisiMuncul = new Vector3(posisiAwal.x, posisiAwal.y + offsetMuncul, posisiAwal.z);
        transform.position = posisiSembunyi;
    }

    void Update()
    {
        DeteksiPemain();
        GerakkanSpike();
    }

    void DeteksiPemain()
    {
        if (pemain == null) return;
        float jarak = Vector2.Distance(transform.position, pemain.position);
        bool pemainDekat = jarak <= deteksiRadius;
        if (!pemainDekat)
        {
            sudahAktif = false;
            sedangNaik = false;
            return;
        }
        if (pemainDekat && !sudahAktif)
        {
            sudahAktif = true;
            StartCoroutine(NaikSetelahDelay());
        }
    }

    System.Collections.IEnumerator NaikSetelahDelay()
    {
        yield return new WaitForSeconds(delayMuncul);
        sedangNaik = true;

        // Putar suara saat spike muncul
        if (suaraMuncul != null && audioSource != null)
            audioSource.PlayOneShot(suaraMuncul);
    }

    void GerakkanSpike()
    {
        Vector3 target = sedangNaik ? posisiMuncul : posisiSembunyi;
        float kecepatan = sedangNaik ? kecepatanNaik : kecepatanTurun;
        transform.position = Vector3.MoveTowards(
            transform.position,
            target,
            kecepatan * Time.deltaTime
        );
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameOver gameOver = Object.FindFirstObjectByType<GameOver>();
            if (gameOver != null)
            {
                gameOver.TriggerGameOver();
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, deteksiRadius);
    }
}