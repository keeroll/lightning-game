using UnityEngine;
using System.Collections;

public class LightningManager : MonoBehaviour
{
    [Header("Lightning Settings")]
    public GameObject lightningPrefab; // We'll create this
    public float strikeInterval = 3f;
    public float strikeRadius = 15f;
    public int simultaneousStrikes = 1;

    [Header("Player Reference")]
    public Transform player;

    [Header("Strike Area")]
    public Transform groundPlane;

    [Header("Warning System")]
    public LightningWarning warningSystem;

    private Coroutine lightningCoroutine;

    void Start()
    {
        if (player == null)
            player = FindAnyObjectByType<PlayerController>().transform;

        StartLightningStrikes();
    }

    public void StartLightningStrikes()
    {
        if (lightningCoroutine != null)
            StopCoroutine(lightningCoroutine);

        lightningCoroutine = StartCoroutine(LightningStrikeLoop());
    }

    IEnumerator LightningStrikeLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(strikeInterval);

            for (int i = 0; i < simultaneousStrikes; i++)
            {
                Vector3 strikePosition = GetRandomStrikePosition();
                CreateLightningStrike(strikePosition);
            }
        }
    }

    Vector3 GetRandomStrikePosition()
    {
        // Random position within strike radius
        Vector2 randomCircle = Random.insideUnitCircle * strikeRadius;
        Vector3 worldPos = groundPlane.position + new Vector3(randomCircle.x, 0, randomCircle.y);

        // Make sure it's on the ground
        if (Physics.Raycast(worldPos + Vector3.up * 10f, Vector3.down, out RaycastHit hit, 20f))
        {
            return hit.point;
        }

        return worldPos;
    }

    void CreateLightningStrike(Vector3 position)
    {
        if (warningSystem != null)
        {
            warningSystem.StartWarningSequence(position);
        }
        else if (lightningPrefab != null)
        {
            // Fallback to immediate strike
            Instantiate(lightningPrefab, position, Quaternion.identity);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (groundPlane != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(groundPlane.position, strikeRadius);
        }
    }
}