using UnityEngine;

[RequireComponent(typeof(TentacleAnimation))]
public class TentacleIK : MonoBehaviour
{
    public int iterations = 10;
    public float threshold = 0.01f;
    public float boneLength = 0.4f;
    public float maxDeltaPerIteration = 15f;
    public float maxTotalRotation = 30f;

    private TentacleAnimation anim;
    private Vector3[] joints;

    void Start()
    {
        anim = GetComponent<TentacleAnimation>();
        joints = new Vector3[anim.rotations.Length + 1];
    }

    void Update()
    {
        int count = anim.rotations.Length;
        Vector3 origin = transform.position;
        Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        target.z = 0;

        // Reconstruction initiale à partir des rotations actuelles
        joints[0] = origin;
        float cumulativeAngle = 0f;
        for (int i = 0; i < count; i++)
        {
            cumulativeAngle += anim.rotations[i];
            float angleRad = cumulativeAngle * Mathf.Deg2Rad;
            Vector3 dir = new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad), 0);
            joints[i + 1] = joints[i] + dir * boneLength;
        }

        // CCD
        for (int iter = 0; iter < iterations; iter++)
        {
            for (int i = count - 1; i >= 0; i--)
            {
                Vector3 joint = joints[i];
                Vector3 toEnd = (joints[count] - joint).normalized;
                Vector3 toTarget = (target - joint).normalized;

                float angleDelta = Vector2.SignedAngle(toEnd, toTarget);
                angleDelta = Mathf.Clamp(angleDelta, -maxDeltaPerIteration, maxDeltaPerIteration);

                Quaternion rot = Quaternion.Euler(0f, 0f, angleDelta);
                for (int j = i + 1; j <= count; j++)
                {
                    joints[j] = joint + rot * (joints[j] - joint);
                }
            }

            if ((joints[count] - target).magnitude < threshold)
                break;
        }

        // Appliquer les rotations et les contraindre
        for (int i = 0; i < count; i++)
        {
            Vector3 dir = joints[i + 1] - joints[i];
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            if (i == 0)
            {
                anim.rotations[i] = angle; // pivot libre
            }
            else
            {
                anim.rotations[i] = Mathf.Clamp(angle, -maxTotalRotation, maxTotalRotation); // clamp dur
            }
        }
    }
}