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
                PlayerHealth playerHealth = hitCollider.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(1);
                    Debug.Log("Player struck by lightning and took damage!");
                }
                else
                {
                    Debug.Log("Player hit but no PlayerHealth component found!");
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