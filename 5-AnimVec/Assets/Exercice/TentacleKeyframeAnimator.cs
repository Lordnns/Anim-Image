using UnityEngine;

[RequireComponent(typeof(TentacleAnimation))]
public class TentacleKeyframeAnimator : MonoBehaviour
{
    [System.Serializable]
    public struct Keyframe
    {
        public float time;              // Temps de la keyframe en secondes
        public float[] rotations;       // Une rotation par os (Z uniquement)
    }

    public Keyframe[] keyframes;
    public bool loop = true;

    private TentacleAnimation anim;
    private float timer;

    void Start()
    {
        anim = GetComponent<TentacleAnimation>();

        // Initialise le tableau de rotations avec la taille du premier keyframe
        if (keyframes.Length >= 1)
        {
            anim.rotations = new float[keyframes[0].rotations.Length];
        }
    }

    void Update()
    {
        if (keyframes.Length < 2) return;

        timer += Time.deltaTime;
        float duration = keyframes[keyframes.Length - 1].time;

        if (loop)
            timer %= duration;
        else
            timer = Mathf.Min(timer, duration);

        // Trouve les deux keyframes à interpoler
        int i = 0;
        while (i < keyframes.Length - 1 && keyframes[i + 1].time < timer)
            i++;

        var kf0 = keyframes[i];
        var kf1 = keyframes[Mathf.Min(i + 1, keyframes.Length - 1)];

        float t = Mathf.InverseLerp(kf0.time, kf1.time, timer);

        // Interpolation des rotations pour chaque os
        for (int b = 0; b < anim.rotations.Length; b++)
        {
            float r0 = kf0.rotations[b];
            float r1 = kf1.rotations[b];
            anim.rotations[b] = Mathf.Lerp(r0, r1, t);
        }
    }
}
