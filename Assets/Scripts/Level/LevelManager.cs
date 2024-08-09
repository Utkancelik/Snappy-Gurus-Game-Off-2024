using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Level
{
    public class LevelManager : MonoBehaviour
    {
        public static LevelManager Instance;

        private List<LevelButton> _levelButtons = new List<LevelButton>();
        LevelIndex _currentLevelIndex = 0;
        [SerializeField] private LevelsScriptableObject _levelsSO;

        [System.Serializable]
        public class LevelEmotion
        {
            public int levelNumber;
            public EmotionController.EmotionState emotionToIncrease;
        }

        public List<LevelEmotion> levelEmotions;
        private Dictionary<int, EmotionController.EmotionState> levelEmotionDictionary;
        private HashSet<int> completedLevels;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                InitializeLevelEmotions();
                LoadCompletedLevels();
            }
            else
            {
                Destroy(gameObject);
            }

            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            InitializeLevelButtons();
        }

        private void LoadGameScene(LevelIndex currentLevelIndex)
        {
            PlayerPrefs.SetInt("CurrentLevelIndex", (int)currentLevelIndex);
            SceneManager.LoadScene((int)SceneIndex.Game);
        }

        private void InitializeLevelEmotions()
        {
            levelEmotionDictionary = new Dictionary<int, EmotionController.EmotionState>();
            foreach (var levelEmotion in levelEmotions)
            {
                levelEmotionDictionary[levelEmotion.levelNumber] = levelEmotion.emotionToIncrease;
            }
        }

        private void LoadCompletedLevels()
        {
            completedLevels = new HashSet<int>();
            string completedLevelsStr = PlayerPrefs.GetString("CompletedLevels", "");
            if (!string.IsNullOrEmpty(completedLevelsStr))
            {
                string[] levels = completedLevelsStr.Split(',');
                foreach (var level in levels)
                {
                    if (int.TryParse(level, out int levelNumber))
                    {
                        completedLevels.Add(levelNumber);
                    }
                }
            }
        }

        private void SaveCompletedLevels()
        {
            string completedLevelsStr = string.Join(",", completedLevels);
            PlayerPrefs.SetString("CompletedLevels", completedLevelsStr);
            PlayerPrefs.Save();
        }

        public void LevelCompleted(EmotionController.Character character)
        {
            int currentLevel = GetCurrentLevel();
            if (!completedLevels.Contains(currentLevel) && levelEmotionDictionary.ContainsKey(currentLevel))
            {
                EmotionController.EmotionState emotion = levelEmotionDictionary[currentLevel];
                EmotionController.Instance.UpdatePlayerPrefs(character, emotion);
                completedLevels.Add(currentLevel);
                SaveCompletedLevels();
            }
        }

        private int GetCurrentLevel()
        {
            return PlayerPrefs.GetInt("CurrentLevelIndex", 0);
        }

        public void InitializeLevelButtons()
        {
            _levelButtons.Clear();
            GameObject panel = GameObject.Find("Panel");
            Debug.Log(panel.name);
            foreach (Transform level in panel.transform)
            {
                LevelButton levelButton = level.GetComponent<LevelButton>();
                if (levelButton != null)
                {
                    _levelButtons.Add(levelButton);
                    levelButton.OnLevelButtonClicked += LoadGameScene;
                }
            }
        }
    }
}
