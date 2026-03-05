using UnityEngine;
public class RandomSfxPlayer : MonoBehaviour
{
    public AudioSource source;
    [Header("Hit sets")]
    public AudioClip[] hit;
    public AudioClip[] damage;
    [Header("Randomization")]
    public Vector2 pitchRange = new Vector2(0.95f, 1.05f);
    public Vector2 volumeRange = new Vector2(0.90f, 1.00f);
    public void PlayHit() => PlayFrom(hit);
    public void PlayDamage() => PlayFrom(damage);
    void PlayFrom(AudioClip[] clips)
    {
        if (clips == null || clips.Length == 0 || source == null) return;
        var clip = clips[Random.Range(0, clips.Length)];
        source.pitch = Random.Range(pitchRange.x, pitchRange.y);
        source.volume = Random.Range(volumeRange.x, volumeRange.y);
        source.PlayOneShot(clip);
    }
}
