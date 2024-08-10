using UnityEngine;

public class MainMenuUIController : MonoBehaviour
{
    [System.Serializable]
    public class CharacterUI
    {
        public UnityEngine.UI.Image happinessImage;
        public TMPro.TMP_Text happinessText;
        public UnityEngine.UI.Image angerImage;
        public TMPro.TMP_Text angerText;
    }

    public CharacterUI alexUI;
    public CharacterUI samUI;

    private void Start()
    {
        if (Level.LevelManager.Instance != null)
        {
            Level.LevelManager.Instance.InitializeLevelButtons();
        }
        UpdateEmotionUI();
        AudioManager.Instance.PlayMusicClip("MainMenuTheme");
    }

    private void UpdateEmotionUI()
    {
        UpdateCharacterUI(EmotionController.Character.Alex, alexUI);
        UpdateCharacterUI(EmotionController.Character.Sam, samUI);
    }

    private void UpdateCharacterUI(EmotionController.Character character, CharacterUI characterUI)
    {
        UpdateEmotionValue(character, EmotionController.EmotionState.Happiness, characterUI.happinessText);
        UpdateEmotionValue(character, EmotionController.EmotionState.Anger, characterUI.angerText);
    }

    private void UpdateEmotionValue(EmotionController.Character character, EmotionController.EmotionState emotionState, TMPro.TMP_Text textComponent)
    {
        int value = EmotionController.Instance.GetEmotionValue(character, emotionState);
        textComponent.text = $"{value}";

        if (EmotionController.Instance.IsEmotionChanged(character, emotionState))
        {
            StartCoroutine(PlayPulseEffect(textComponent));
            EmotionController.Instance.ResetEmotionChangedFlag(character, emotionState);
        }
    }

    private System.Collections.IEnumerator PlayPulseEffect(TMPro.TMP_Text textComponent)
    {
        float duration = 2f;
        float elapsed = 0f;
        Vector2 originalScale = textComponent.transform.localScale;

        while (elapsed < duration)
        {
            elapsed += UnityEngine.Time.deltaTime;
            float scale = Mathf.Lerp(1f, 2f, Mathf.PingPong(elapsed * 2f, 1f));
            textComponent.transform.localScale = originalScale * scale;
            yield return null;
        }

        textComponent.transform.localScale = originalScale;
    }
}
