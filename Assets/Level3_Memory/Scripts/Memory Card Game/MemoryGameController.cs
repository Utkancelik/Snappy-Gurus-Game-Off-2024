using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MemoryGameController : MonoBehaviour
{
    public GameObject chefCharacter;
    public GameObject playerCharacter;
    public TMP_Text dialogueText;
    public GameObject cardPanel;
    public GameObject tablePanel;
    private static readonly int Walk = Animator.StringToHash("Walk");

    void Start()
    {
        StartCoroutine(StartGameSequence());
    }

    IEnumerator StartGameSequence()
    {
        // Şefin oyuncuya yürüsün, animasyon ya da kodla
        chefCharacter.GetComponent<Animator>().SetTrigger(Walk);
        yield return new WaitForSeconds(2.5f); // yürüme süresi

        // diyalog için balon çıkar
        chefCharacter.transform.Find("Canvas/DialogueBaloon").gameObject.SetActive(true);
        dialogueText.text = "Siparişiniz karıştı, lütfen tekrar sipariş verin.";
        dialogueText.gameObject.SetActive(true);

        yield return new WaitForSeconds(5); // diyalog süresi

        // diyalog baloncuğunu kapat ve oyunu başlat.
        chefCharacter.transform.Find("Canvas/DialogueBaloon").gameObject.SetActive(false);
        dialogueText.gameObject.SetActive(false);
        cardPanel.SetActive(true);
        tablePanel.SetActive(true);
        
        MemoryGame memoryGame = GetComponent<MemoryGame>();
        memoryGame.StartGame();
    }
}