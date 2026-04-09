using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Stats")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 15f;
    [SerializeField] private float fallThreshold = -3f;

    private Rigidbody2D rb;
    private Animator anim;
    private bool isGrounded = false;
    private int sessionCoins = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Debug.Log("<color=#00ff00>[Player] Initialized.</color>");
    }

    void Update()
    {
        // Only allow movement and fall checks if the game is active
        if (GameManager.instance != null && !GameManager.instance.isLevelActive) return;

        Move();
        CheckFall();
    }

    private void Move()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        if (moveInput > 0) transform.localScale = new Vector3(1, 1, 1);
        else if (moveInput < 0) transform.localScale = new Vector3(-1, 1, 1);

        anim.SetFloat("Speed", Mathf.Abs(moveInput));

        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isGrounded = false;
            anim.SetBool("IsJumping", true);
        }
    }

    private void CheckFall()
    {
        if (transform.position.y < fallThreshold)
        {
            Debug.Log($"<color=red>[Gameplay] FAIL! Player Y ({transform.position.y}) fell below threshold ({fallThreshold})</color>");
            if (AudioManager.instance != null) AudioManager.instance.PlayFail();
            if (GameManager.instance != null) GameManager.instance.ResetToMainMenu();
            sessionCoins = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            anim.SetBool("IsJumping", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Safety check: Don't trigger if the level is already resetting
        if (GameManager.instance != null && !GameManager.instance.isLevelActive) return;

        if (collision.gameObject.CompareTag("Coin"))
        {
            Debug.Log("<color=#00ff00>[Gameplay] Coin Picked Up.</color>");
            CollectCoin(1);

            // --- FIX: SetActive(false) instead of Destroy ---
            collision.gameObject.SetActive(false);
        }
        else if (collision.gameObject.CompareTag("Chest"))
        {
            Debug.Log("<color=#00ff00>[Gameplay] WIN! Chest reached.</color>");
            CollectCoin(10);
            if (AudioManager.instance != null) AudioManager.instance.PlayWin();

            // Reset must happen AFTER the bonus is added
            if (GameManager.instance != null) GameManager.instance.ResetToMainMenu();
            sessionCoins = 0;
        }
    }

    private void CollectCoin(int amount)
    {
        sessionCoins += amount;
        PrefsManager.AddCoins(amount);
        if (UIManager.instance != null)
        {
            UIManager.instance.UpdateHudSessionCoins(sessionCoins);
            UIManager.instance.UpdateMainMenuTotal(PrefsManager.GetCoins());
        }
        if (amount == 1 && AudioManager.instance != null) AudioManager.instance.PlayCoin();
    }
}