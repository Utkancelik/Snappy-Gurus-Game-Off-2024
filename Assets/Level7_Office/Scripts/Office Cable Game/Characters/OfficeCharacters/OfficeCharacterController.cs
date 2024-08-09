using System;
using System.Collections;
using UnityEngine;
using TMPro;

public class OfficeCharacterController : MonoBehaviour
{
    [SerializeField] private TMP_Text characterText;
    [SerializeField] private Sprite celebrateSprite;
    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private float jumpHeight = 2f; 
    [SerializeField] private float jumpDuration = 0.5f;
    [SerializeField] private string[] blackOutDialogues;
    [SerializeField] private string[] celebrationDialogues;

    public static Action OnBlackOut;
    public static Action OnRepairComplete;

    private SpriteRenderer spriteRenderer;
    private bool isBlackedOut = false;

    private Animator _animator;
    private static readonly int CelebrateTrigger = Animator.StringToHash("Celebrate");

    private void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _animator = GetComponentInChildren<Animator>();
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
        StartCoroutine(ShowBlackOutDialogueCoroutine());
    }

    private IEnumerator ShowBlackOutDialogueCoroutine()
    {
        if (FindObjectOfType<OfficeCableGameController>()._isBlackedOut)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(0.5f, 3f));
            characterText.text = GetRandomDialogue(blackOutDialogues);
            yield return new WaitForSeconds(UnityEngine.Random.Range(0.5f, 3f));
            characterText.text = "";
            StartCoroutine(ShowBlackOutDialogueCoroutine());
        }
    }

    private string GetRandomDialogue(string[] dialogues)
    {
        return dialogues[UnityEngine.Random.Range(0, dialogues.Length)];
    }

    private void StartCelebration()
    {
        Celebrate();
    }

    private void Celebrate()
    {
        characterText.text = GetRandomDialogue(celebrationDialogues);
        spriteRenderer.sprite = celebrateSprite;
        _animator.SetTrigger(CelebrateTrigger);
        Invoke(nameof(Celebrate), 1f);
    }
}
