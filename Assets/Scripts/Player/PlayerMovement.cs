using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f; 

    private Rigidbody2D rigid;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector2 movementVector = new Vector2(horizontalInput, verticalInput).normalized;
        rigid.linearVelocity = movementVector * moveSpeed;
        animator.SetInteger("YVelocity", (int)rigid.linearVelocityY);

        HandleDirection(horizontalInput);
    }

    public void PlayFootVFX()
    {
        FootDustVfx.instance.EmitDust();
    }


    private void HandleDirection(float horizontalInput)
    {
        if (horizontalInput > 0)
        {
            transform.localScale = new Vector2(-1, 1); 
        }
        else if (horizontalInput < 0)
        {
            transform.localScale = new Vector2( 1, 1);
        }
    }

    private void HandleDepthSorting()
    {
        spriteRenderer.sortingOrder = Mathf.RoundToInt(transform.position.y * -100);
    }
}