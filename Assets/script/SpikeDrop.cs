using UnityEngine;

public class SpikeDrop : MonoBehaviour
{
    public float kecepatanTurun = 8f;
    public float targetY = 0f; // posisi Y tujuan (lantai)

    private bool sedangTurun = false;
    private System.Action onSelesai;

    public void MulaiTurun(float tujuanY, System.Action callback = null)
    {
        targetY = tujuanY;
        onSelesai = callback;
        sedangTurun = true;
    }

    void Update()
    {
        if (!sedangTurun) return;

        transform.position = Vector3.MoveTowards(
            transform.position,
            new Vector3(transform.position.x, targetY, transform.position.z),
            kecepatanTurun * Time.deltaTime
        );

        if (Mathf.Abs(transform.position.y - targetY) < 0.05f)
        {
            transform.position = new Vector3(transform.position.x, targetY, transform.position.z);
            sedangTurun = false;
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