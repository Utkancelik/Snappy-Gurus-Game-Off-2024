using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Level
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private List<LevelButton> _levelButtons; 
        LevelIndex _currentLevelIndex = 0;
        [SerializeField] private LevelsScriptableObject _levelsSO;

        private void Start()
        {
            foreach (var button in _levelButtons)
            {
                button.OnLevelButtonClicked += LoadGameScene;
            } 
        }

        private void LoadGameScene(LevelIndex currentLevelIndex)
        {
            PlayerPrefs.SetInt("CurrentLevelIndex", (int)currentLevelIndex);
            SceneManager.LoadScene((int)SceneIndex.Game);
        }
    }
}
