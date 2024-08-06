using System;
using UnityEngine;
using TMPro;
using System.Collections;

public class OfficeCharacterController : MonoBehaviour
{
    [SerializeField] private TMP_Text characterText;
    [SerializeField] private Sprite celebrateSprite;
    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private float jumpHeight = 2f; // Zıplama yüksekliği
    [SerializeField] private float jumpDuration = 0.5f; // Zıplama süresi

    public static Action OnBlackOut;
    public static Action OnRepairComplete;

    private SpriteRenderer spriteRenderer;
    private bool isJumping = false;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        DeactivateNeccessaryObjectsAtTheBeginning();
    }
    
    private void DeactivateNeccessaryObjectsAtTheBeginning()
    {
        characterText.text = $"";
    }

    private void OnEnable()
    {
        OnBlackOut += ShowBlackOutDialogue;
        OnRepairComplete += StartCelebration;
    }

    private void OnDisable()
    {
        OnBlackOut -= ShowBlackOutDialogue;
        OnRepairComplete -= StartCelebration;
    }

    private void ShowBlackOutDialogue()
    {
        characterText.text = GetRandomBlackOutCharacterDialogue();
    }

    private string GetRandomBlackOutCharacterDialogue()
    {
        string[] dialogues = new[]
            { "Come on!", "Someone please fix this", "AAAAA", "Right on time always..", "Really?" };
        return dialogues[UnityEngine.Random.Range(0, dialogues.Length)];
    }

    private void StartCelebration()
    {
        if (!isJumping)
        {
            StartCoroutine(Celebrate());
        }
    }

    private IEnumerator Celebrate()
    {
        isJumping = true;
        characterText.text = GetRandomCelebrationDialogue();
        spriteRenderer.sprite = celebrateSprite;

        float timeElapsed = 0;
        Vector3 originalPosition = transform.position;

        while (timeElapsed < jumpDuration)
        {
            float newY = Mathf.Sin(Mathf.PI * timeElapsed / jumpDuration) * jumpHeight;
            transform.position = new Vector3(originalPosition.x, originalPosition.y + newY, originalPosition.z);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = originalPosition;
        spriteRenderer.sprite = defaultSprite;
        isJumping = false;

        yield return new WaitForSeconds(0.5f);
        StartCoroutine(Celebrate());
    }

    private string GetRandomCelebrationDialogue()
    {
        string[] dialogues = new[]
            { "Yay!", "Finally!", "Thanks Alex!", "Woohoo!", "Great job!" };
        return dialogues[UnityEngine.Random.Range(0, dialogues.Length)];
    }
}
