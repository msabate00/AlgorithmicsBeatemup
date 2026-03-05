using UnityEngine;

public class VfxSpawner : MonoBehaviour
{
    public ParticleSystem hitImpactPS;
    public static VfxSpawner instance;

    void Awake() => instance = this;

    public void HitImpact(Vector3 worldPos)
    {
        if (!hitImpactPS) return;
        hitImpactPS.transform.position = worldPos;
        hitImpactPS.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        hitImpactPS.Play();
    }
}