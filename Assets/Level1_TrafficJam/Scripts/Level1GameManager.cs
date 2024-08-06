using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class Level1GameManager : MonoBehaviour
{
    // Singleton instance
    public static Level1GameManager Instance { get; private set; }

    // Add your game management variables and methods here
    public int PlayerScore;
    public int ScoreToCollect;
    public bool IsGameOver;
    public UnityEvent OnGameOver;
    public UnityEvent OnScoreChange;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        InitializeGameManager();
    }

    private void InitializeGameManager()
    {
        PlayerScore = 0;
        ScoreToCollect = 10;
    }

    public void UpdatePlayerScore()
    {
        PlayerScore += 1;
        OnScoreChange.Invoke();
        if (PlayerScore == ScoreToCollect)
        {
            TriggerGameOver();
        }
    }

    public void TriggerGameOver()
    {
        IsGameOver = true;
        OnGameOver.Invoke();
    }
}