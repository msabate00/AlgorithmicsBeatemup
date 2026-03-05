using UnityEngine;
using System.Collections;

public class PlayerCombat : MonoBehaviour
{
    public RandomSfxPlayer sfxPlayer;

    [Header("Attack Settings")]
    public float attackDamage = 10f;


    public float attackCooldown = 0.5f; 
    private float nextAttackTime = 0f;

    [Header("References")]
    private Animator animator = null;

    void Awake()
    {
        // cache the animator component
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Time.time >= nextAttackTime)
        {

            if (Input.GetButtonDown("Attack"))
            {
                Attack();
                nextAttackTime = Time.time + attackCooldown;
            }
        }
    }

    void Attack()
    {
        sfxPlayer.PlayHit();

        if (animator != null)
        {
            animator.SetTrigger("Attack");
        }

    }

    //  Handle collision with the attack hitbox
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Empty for now
    }
}