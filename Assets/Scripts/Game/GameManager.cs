using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private LevelsScriptableObject _levelsSO;

    private void Start()
    {
        var currentLevelIndex = PlayerPrefs.GetInt("CurrentLevelIndex");
        foreach (var levelInfo in _levelsSO._levelInfos)
        {
            if (levelInfo.LevelIndex == (LevelIndex)currentLevelIndex)
            {
                Instantiate(levelInfo.LevelPrefab);
            }
        }
        Time.timeScale = 1;
    }
}
