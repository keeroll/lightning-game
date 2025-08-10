using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 3;
    public float invulnerabilityTime = 1f;
    
    private int currentHealth;
    private bool isInvulnerable = false;
    private float invulnerabilityTimer = 0f;
    
    void Start()
    {
        currentHealth = maxHealth;
    }
    
    void Update()
    {
        if (isInvulnerable)
        {
            invulnerabilityTimer -= Time.deltaTime;
            if (invulnerabilityTimer <= 0f)
            {
                isInvulnerable = false;
            }
        }
    }
    
    public void TakeDamage(int damage = 1)
    {
        if (isInvulnerable) return;
        
        currentHealth -= damage;
        isInvulnerable = true;
        invulnerabilityTimer = invulnerabilityTime;
        
        Debug.Log($"Player hit! Health: {currentHealth}/{maxHealth}");
        
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    
    void Die()
    {
        Debug.Log("Player died!");
        // For now, just restart the scene
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
    
    public int GetHealth()
    {
        return currentHealth;
    }
}