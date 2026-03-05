using UnityEngine;
public class FootstepSfx : MonoBehaviour
{
    public AudioSource source;
    public AudioClip[] dirt;
    public Vector2 pitchRange = new Vector2(0.95f, 1.05f);

    public void Footstep()
    {
        if (source == null) return;
        if (dirt == null || dirt.Length == 0) return;
        AudioClip clip = dirt[Random.Range(0, dirt.Length)];
        source.pitch = Random.Range(pitchRange.x, pitchRange.y);
        source.PlayOneShot(clip);
    }
}
