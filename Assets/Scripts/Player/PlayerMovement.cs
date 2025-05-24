using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    private PlayerStats stats;
    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 movement;

    void Start()
    {
        stats = GetComponent<PlayerStats>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        Vector2 input = new Vector2(x, y).normalized;
        movement = input;

        bool isWalking = input.magnitude > 0.1f;
        animator.SetBool("isWalking", isWalking);

        if (isWalking)
        {
            animator.SetFloat("InputX", x);
            animator.SetFloat("InputY", y);
            animator.SetFloat("LastInputX", x);
            animator.SetFloat("LastInputY", y);
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * stats.movementSpeed * Time.fixedDeltaTime);
    }
}
