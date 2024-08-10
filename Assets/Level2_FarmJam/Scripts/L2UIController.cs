using TMPro;
using UnityEngine;

public class L2UIController : MonoBehaviour
{
    [SerializeField] private TMP_Text _counterText;
    private L2GameManager _gameManager;
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = L2GameManager.Instance;
        L2GameManager.Instance.OnScoreChange.AddListener(UpdateScoreText);
        UpdateScoreText();
    }
    
    private void UpdateScoreText()
    {
        _counterText.text = $"{_gameManager.PlayerScore}/{_gameManager.ScoreToCollect}";
    }
}