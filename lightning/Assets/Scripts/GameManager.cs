using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Game State")]
    public Transform player;
    public Transform goal;
    public float goalRadius = 2f;
    
    [Header("UI")]
    public TMP_Text timerText;
    public TMP_Text statusText;
    
    private float gameTimer = 0f;
    private bool gameActive = true;
    
    void Update()
    {
        if (!gameActive) return;
        
        gameTimer += Time.deltaTime;
        UpdateUI();
        CheckWinCondition();
    }

    void UpdateUI()
    {
        if (timerText != null)
        {
            timerText.SetText($"Time: {gameTimer:F1}s");
        }
        
        if (!gameActive && Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }
    
    void CheckWinCondition()
    {
        if (player != null && goal != null)
        {
            float distance = Vector3.Distance(player.position, goal.position);
            if (distance <= goalRadius)
            {
                WinGame();
            }
        }
    }
    
    public void WinGame()
    {
        gameActive = false;
        if (statusText != null)
        {
            statusText.SetText($"YOU WON!\nTime: {gameTimer:F1}s");
            statusText.gameObject.SetActive(true);
        }
        Debug.Log($"Game completed in {gameTimer:F1} seconds!");
    }
    
    public void LoseGame()
    {
        gameActive = false;
        if (statusText != null)
        {
            statusText.SetText("STRUCK BY LIGHTNING!\nPress R to restart");
            statusText.gameObject.SetActive(true);
        }
    }
    
    void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}