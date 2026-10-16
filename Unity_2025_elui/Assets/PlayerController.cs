using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
public class PlayerController : MonoBehaviour
{
    public TMP_Text livesText;
    public string playerName = "Player";
    public int hp = 100;
    public int lives = 3;
    public float moveSpeed = 5f;
    public float jumpPower = 8f;
    public float fallLimit = -10f;
    public Vector3 startPosition;
    public Transform visual;
    public Vector2 airScale = new Vector2(0.8f, 1.2f);
    private Vector2 moveInput;
    private Rigidbody2D rb;
    private bool isGrounded = false;
    private bool isGameOver = false;
    private float facing = 1f;
    private GameObject gameOverPanel;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        transform.position = startPosition;
        livesText.text="LIFE" + lives);
    }
    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        if (moveInput.x > 0) { facing = 1f; }
        else if (moveInput.x < 0) { facing = -1f; }


    }
    void OnJump(InputValue value)
    {
        if (value.isPressed && isGrounded && !isGameOver)
        {
            Debug.Log("점프!");
            rb.linearVelocity = new Vector2(rb.linearVelocity.x,
            jumpPower);
        }
    }

        void Update()
        {
            if (!isGameOver)
            {
                transform.Translate(Vector3.right * moveInput.x
                * moveSpeed * Time.deltaTime);
            }
            if (transform.position.y < fallLimit)
            {
                LoseLife();
            }
            if (isGrounded)
            { visual.localScale = new Vector3(facing, 1f, 1f); }
            else
            {
                visual.localScale = new Vector3(facing * airScale.x,
              airScale.y, 1f);
            }
        }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        { isGrounded = true; }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        { isGrounded = false; }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Goal"))
        {
            Debug.Log("골인");
        }
        else if (other.CompareTag("Trap"))
        {
            LoseLife();
        }
    }
    void LoseLife()
    {
        lives -= 1;
        transform.position = startPosition;
        rb.linearVelocity = Vector2.zero;
        livesText.text = "LKFE" + lives;
        if (lives <= 0)
        {
            isGameOver = true;
            gameOverPanel.SetActive(true);
        }
    }
}   
