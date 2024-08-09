using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class L1GameManager : MonoBehaviour
{
    // Singleton instance
    public static L1GameManager Instance { get; private set; }

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
    }

    public void UpdatePlayerScore()
    {
        PlayerScore += 1;
        OnScoreChange.Invoke();
        if (PlayerScore == ScoreToCollect)
        {
            TriggerWinGame();
        }
    }

    public void TriggerWinGame()
    {
        UIManager.Instance.ToggleWinMenu();
        IsGameOver = true;
        OnGameOver.Invoke();
    }
    public void TriggerLoseGame()
    {
        UIManager.Instance.ToggleLoseMenu();
        IsGameOver = true;
        OnGameOver.Invoke();
    }
}