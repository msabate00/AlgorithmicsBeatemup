using UnityEngine;

public class EnemyChaseState : State
{
    private Enemy enemy;
    private Rigidbody2D rigid => enemy.rigid;

    public float chaseSpeed = 4f;
    public float atkDistance = 2f;

    void Start()
    {
        enemy = GetComponentInParent<Enemy>();
    }

    public override void LogicUpdate()
    {
        Vector2 directionToPlayer = (enemy.player.transform.position - transform.position).normalized;

        rigid.linearVelocity = directionToPlayer * chaseSpeed;

        if (Vector2.Distance(transform.position, enemy.player.transform.position) <= atkDistance)
        {
            TransitionTo("EnemyAttackState");
        }
    }

}
