using System;
using UnityEngine;

public class EmotionController : MonoBehaviour
{
    public static EmotionController Instance;

    public enum EmotionState
    {
        Happiness,
        Anger,
    }

    public enum Character
    {
        Alex,
        Sam,
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    public void UpdatePlayerPrefs(Character character, EmotionState emotionState)
    {
        string parameter = GetParameterName(character, emotionState);
        int currentValue = PlayerPrefs.GetInt(parameter, 0);
        PlayerPrefs.SetInt(parameter, currentValue + 1);

        // Değişiklik bayrağını ayarla
        PlayerPrefs.SetInt("Changed_" + parameter, 1);

        PlayerPrefs.Save();
    }

    public int GetEmotionValue(Character character, EmotionState emotionState)
    {
        string parameter = GetParameterName(character, emotionState);
        return PlayerPrefs.GetInt(parameter, 0);
    }

    public bool IsEmotionChanged(Character character, EmotionState emotionState)
    {
        string parameter = "Changed_" + GetParameterName(character, emotionState);
        return PlayerPrefs.GetInt(parameter, 0) == 1;
    }

    public void ResetEmotionChangedFlag(Character character, EmotionState emotionState)
    {
        string parameter = "Changed_" + GetParameterName(character, emotionState);
        PlayerPrefs.SetInt(parameter, 0);
        PlayerPrefs.Save();
    }

    private string GetParameterName(Character character, EmotionState emotionState)
    {
        return $"{character}_{emotionState}";
    }
}