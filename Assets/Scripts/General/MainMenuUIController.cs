using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUIController : MonoBehaviour
{
    [System.Serializable]
    public class CharacterUI
    {
        public Image happinessImage;
        public TMP_Text happinessText;
        public Image angerImage;
        public TMP_Text angerText;
    }

    public CharacterUI alexUI;
    public CharacterUI samUI;

    private void Start()
    {
        // İlk değerleri al ve sakla
        UpdateEmotionUI();
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

    private void UpdateEmotionValue(EmotionController.Character character, EmotionController.EmotionState emotionState, TMP_Text textComponent)
    {
        int value = EmotionController.Instance.GetEmotionValue(character, emotionState);
        textComponent.text = $"{value}";

        if (EmotionController.Instance.IsEmotionChanged(character, emotionState))
        {
            StartCoroutine(PlayPulseEffect(textComponent));
            EmotionController.Instance.ResetEmotionChangedFlag(character, emotionState);
        }
    }

    private IEnumerator PlayPulseEffect(TMP_Text textComponent)
    {
        float duration = 2f;
        float elapsed = 0f;
        Vector2 originalScale = textComponent.transform.localScale;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float scale = Mathf.Lerp(1f, 2f, Mathf.PingPong(elapsed * 2f, 1f));
            textComponent.transform.localScale = originalScale * scale;
            yield return null;
        }

        textComponent.transform.localScale = originalScale;
    }

}
