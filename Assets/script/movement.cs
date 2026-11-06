using UnityEngine;
public class movement : MonoBehaviour
{
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    private float horizontal;
    private float speed = 17f;
    private float jumpingPower = 19f;
    public bool isFacingRight = true;
    [HideInInspector] public bool isReversed = false;
    private Rigidbody2D rb;

    [Header("Audio")]
    [SerializeField] private AudioClip jumpSound;   // ← drag file audio di Inspector
    [SerializeField] private Animator animator;       // ← komponen Animator untuk mengontrol animasi
    private AudioSource audioSource;                // ← komponen audio player

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>(); // ← ambil komponen AudioSource
    }

    public void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (!isFacingRight && horizontal > 0f) Flip();
        else if (isFacingRight && horizontal < 0f) Flip();
        float moveInput = isReversed ? -horizontal : horizontal;

        if (moveInput != 0)
        {
            animator.SetBool("isrun", true); // Set parameter isRunning di Animator
        }
        else
        {
            animator.SetBool("isrun", false); // Reset parameter isRunning saat tidak bergerak
        }

        
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpingPower);
            if (jumpSound != null)
                audioSource.PlayOneShot(jumpSound);


        }

        horizontal = moveInput;
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.5f, groundLayer);
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
}