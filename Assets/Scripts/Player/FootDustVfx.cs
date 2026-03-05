using UnityEngine;

public class FootDustVfx : MonoBehaviour
{
    public static FootDustVfx instance;

    public ParticleSystem footDustPS;
    public int burstCount = 12;

    void Awake()
    {
        instance = this;
    }

    public void EmitDust()
    {
        if (!footDustPS) return;
        footDustPS.Emit(burstCount);
    }
}