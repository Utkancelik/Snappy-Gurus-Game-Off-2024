using System;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    [SerializeField] private LevelIndex levelIndex;
    [SerializeField] private Button _button;
    public Action<LevelIndex> OnLevelButtonClicked;
    
    private void Start()
    {
        _button.onClick.AddListener(SendLevelInfo);
    }

    public void SendLevelInfo()
    {
        OnLevelButtonClicked.Invoke(levelIndex);
    }
}
