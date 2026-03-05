using UnityEngine;

public class Enemy : MonoBehaviour
{
    // All enemies have hp and access to these variables
    public int hp = 2;
    public Rigidbody2D rigid;
    public Animator animator;
    public SpriteRenderer sprite;
    public PlayerMovement player;
    public StateMachine stateMachine;

    protected virtual void Awake()
    {
        rigid = GetComponentInChildren<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        player = FindObjectOfType<PlayerMovement>();
        stateMachine = GetComponentInChildren<StateMachine>();
    }

    protected void HandleDirection()
    {
        // Flip the sprite to face the direction of movement
        if (rigid.linearVelocityX > 0)
        {
            transform.localScale = new Vector2(1, 1); // Facing Right
        }
        else if (rigid.linearVelocityX < 0)
        {
            transform.localScale = new Vector2(-1, 1);  // Facing Left
        }

        animator.SetFloat("YVelocity", rigid.linearVelocityY);
    }

    protected virtual void ReceiveDamage()
    {
        
        hp--;

        if (hp <= 0)
        {
           Die();
        }
    }

    protected virtual void ReceiveKnockBack()
    {
        // Transition to Hit State
        animator.SetTrigger("Hit");
        stateMachine.OnChildTransition(stateMachine.CurrentState,"EnemyHitState", new() { ["hit_mode"] = "knockback" });
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("PlayerAttack")) return;

        Vector3 hitPos = collision.ClosestPoint(transform.position);
        VfxSpawner.instance.HitImpact(hitPos);

        ReceiveDamage();
        ReceiveKnockBack();
    }

    void Die()
    {
       Destroy(gameObject);
    }


    void AlignYToTarget()
    {

    }

}
