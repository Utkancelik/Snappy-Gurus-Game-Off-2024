using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class L1UIController : MonoBehaviour
{
    [SerializeField] private TMP_Text _counterText;
    private L1GameManager _gameManager;
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = L1GameManager.Instance;
        L1GameManager.Instance.OnScoreChange.AddListener(UpdateScoreText);
        UpdateScoreText();
    }
    
    private void UpdateScoreText()
    {
        _counterText.text = $"{_gameManager.PlayerScore}/{_gameManager.ScoreToCollect}";
    }
}
