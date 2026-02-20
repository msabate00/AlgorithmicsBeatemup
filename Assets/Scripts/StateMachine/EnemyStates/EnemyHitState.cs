using System.Collections.Generic;
using UnityEngine;

public class EnemyHitState : State
{
    private Enemy enemy;
    private Rigidbody2D rigid => enemy.rigid;
    private SpriteRenderer spriteRenderer => enemy.sprite;

    [Header("Knockback Settings")]
    public float groundDrag = 2f;      // How fast they slow down sliding
    public float gravity = 30f;        // Custom "Fake" gravity strength
    public float initialJumpForce = 8f;// How high they fly visually
    public float slideSpeed = 5f;      // How fast they move back

    // State Variables
    private Vector2 pushDirection;
    private float verticalVelocity;
    private float currentHeight;
    private bool isAirborne;

    public State stateToTransitionTo;

    void Start()
    {
        enemy = GetComponentInParent<Enemy>();
    }

    public override void Enter(Dictionary<string, object> extraArgs = null)
    {
        currentHeight = 0;
        isAirborne = false;
        rigid.linearVelocity = Vector2.zero; // Reset physics

        string hitMode = "normal";
        if (extraArgs != null && extraArgs.ContainsKey("hit_mode"))
        {
            hitMode = (string)extraArgs["hit_mode"];
        }

        if (hitMode == "knockback")
        {
            // Calculate Direction (Away from player)
            pushDirection = (transform.position - enemy.player.transform.position).normalized;

            // Set Initial Velocities
            verticalVelocity = initialJumpForce; // Launch UP (Fake Z)
            rigid.linearVelocity = pushDirection * slideSpeed; // Slide BACK (Ground)
            isAirborne = true;
        }
        else
        {
            TransitionTo(stateToTransitionTo.name); // Recover to selected state
        }
    }

    public override void LogicUpdate()
    {
        if (isAirborne)
        {
            HandleKnockbackPhysics();
        }
    }

    private void HandleKnockbackPhysics()
    {
        // Handle Fake Height (The Arc)
        verticalVelocity -= gravity * Time.deltaTime; // Apply fake gravity
        currentHeight += verticalVelocity * Time.deltaTime;

        // Apply to Sprite only (not the Collider/Rigidbody!)
        spriteRenderer.transform.localPosition = new Vector3(0, currentHeight, 0);

        // Handle Ground Sliding
        rigid.linearVelocity = Vector2.Lerp(rigid.linearVelocity, Vector2.zero, groundDrag * Time.deltaTime);

        // Check for Landing
        if (currentHeight <= 0)
        {
            Land();
        }
    }

    private void Land()
    {
        isAirborne = false;
        currentHeight = 0;
        verticalVelocity = 0;
        rigid.linearVelocity = Vector2.zero; // Stop sliding instantly on land

        // Snap sprite back to 0,0 local position
        spriteRenderer.transform.localPosition = Vector3.zero;

        // Go back to fighting logic
        TransitionTo(stateToTransitionTo.name);
    }

    // Ensure sprite is reset if state exits forcefully
    public override void Exit()
    {
        spriteRenderer.transform.localPosition = Vector3.zero;
        rigid.linearVelocity = Vector2.zero;
    }
}