using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class MusicDirector : MonoBehaviour
{
    [Header("Audio Sources")]
    public AudioSource exploration_AS;
    public AudioSource combat_AS;

    [Header("Mixer Snapshots")]
    public AudioMixerSnapshot exploration;
    public AudioMixerSnapshot combat;

    [Range(0.05f, 2f)] public float transitionSeconds = 0.35f;

    bool inCombat;
    Coroutine co;

    private void Update() { if (Input.GetKeyDown(KeyCode.C)) { SetCombat(!inCombat); } }

    public void SetCombat(bool value)
    {
        if (inCombat == value) return;
        inCombat = value;

        (inCombat ? combat : exploration).TransitionTo(transitionSeconds);

        if (co != null) StopCoroutine(co);
        co = StartCoroutine(Fade(inCombat ? 0f : 1f, inCombat ? 1f : 0f));
    }

    IEnumerator Fade(float eTarget, float cTarget)
    {
        float t = 0f, e0 = exploration_AS.volume, c0 = combat_AS.volume;

        while (t < transitionSeconds)
        {
            t += Time.deltaTime;
            float k = Mathf.Clamp01(t / transitionSeconds);
            exploration_AS.volume = Mathf.Lerp(e0, eTarget, k);
            combat_AS.volume = Mathf.Lerp(c0, cTarget, k);
            yield return null;
        }

        exploration_AS.volume = eTarget;
        combat_AS.volume = cTarget;
    }
}

