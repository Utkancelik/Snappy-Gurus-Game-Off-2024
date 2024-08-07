using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Level1UIController : MonoBehaviour
{
    [SerializeField] private TMP_Text _counterText;
    private Level1GameManager _gameManager;
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = Level1GameManager.Instance;
        Level1GameManager.Instance.OnScoreChange.AddListener(UpdateScoreText);
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        _counterText.text = $"{_gameManager.PlayerScore}/{_gameManager.ScoreToCollect}";
    }
}
