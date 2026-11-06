using UnityEngine;

public class JebakanBiasa : MonoBehaviour
{

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