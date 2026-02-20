using UnityEngine;

public class Longmoan : Enemy
{
    public float chaseSpeed = 4f;
    public float atkDistance = 2f;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Update()
    {
        HandleDirection();
    }
}
