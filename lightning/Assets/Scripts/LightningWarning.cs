using UnityEngine;
using System.Collections;

public class LightningWarning : MonoBehaviour
{
    [Header("Warning Settings")]
    public float warningDuration = 2f;
    public GameObject warningPrefab; // Red circle or similar
    public GameObject lightningPrefab;
    
    [Header("Visual Effects")]
    public AnimationCurve warningPulse = AnimationCurve.EaseInOut(0, 0.5f, 1, 1);
    
    public void StartWarningSequence(Vector3 position)
    {
        StartCoroutine(WarningSequence(position));
    }
    
    IEnumerator WarningSequence(Vector3 position)
    {
        // Create warning visual
        GameObject warning = null;
        if (warningPrefab != null)
        {
            warning = Instantiate(warningPrefab, position, Quaternion.identity);
        }
        
        // Animate warning for duration
        float timer = 0f;
        while (timer < warningDuration)
        {
            timer += Time.deltaTime;
            
            if (warning != null)
            {
                float normalizedTime = timer / warningDuration;
                float pulseValue = warningPulse.Evaluate(normalizedTime);
                warning.transform.localScale = Vector3.one * pulseValue;
                
                // Change color intensity over time
                Renderer renderer = warning.GetComponent<Renderer>();
                if (renderer != null)
                {
                    Color color = Color.red;
                    color.a = 1f - normalizedTime; // Fade out
                    renderer.material.color = color;
                }
            }
            
            yield return null;
        }
        
        // Destroy warning
        if (warning != null)
            Destroy(warning);
            
        // Create actual lightning strike
        if (lightningPrefab != null)
        {
            Instantiate(lightningPrefab, position, Quaternion.identity);
        }
    }
}