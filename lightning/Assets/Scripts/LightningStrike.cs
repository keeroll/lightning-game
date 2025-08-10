using UnityEngine;

public class LightningStrike : MonoBehaviour
{
    [Header("Lightning Properties")]
    public float damageRadius = 2f;
    public float lifetime = 1f;
    public AnimationCurve scaleCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    private float timer = 0f;
    private Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale;
        transform.localScale = Vector3.zero;

        // Check for player damage on impact
        CheckForPlayerDamage();

        // Destroy after lifetime
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // Animate scale
        timer += Time.deltaTime;
        float normalizedTime = timer / lifetime;
        float scaleMultiplier = scaleCurve.Evaluate(normalizedTime);
        transform.localScale = originalScale * scaleMultiplier;
    }

    void CheckForPlayerDamage()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, damageRadius);

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Player"))
            {
                // Player hit by lightning!
                PlayerController player = hitCollider.GetComponent<PlayerController>();
                if (player != null)
                {
                    // For now, just log it. Later we'll add health/death system
                    Debug.Log("Player struck by lightning!");
                }
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, damageRadius);
    }
}