using System.Collections;
using Level;
using UnityEngine;

public class OfficeCableGameController : MonoBehaviour
{
    [SerializeField] private GameObject _officeBG; // Karartılacak sonra aydınlatılacak olan background resmi
    [SerializeField] private GameObject _repairGameWindow;
    [SerializeField] private GameObject _repairZoneIndicator; // Tamir bölgesini işaret edecek obje
    [SerializeField] public GameObject _endRepairGameButton;
    public EmotionController.Character character;
    public bool _isBlackedOut { get; private set; } = false;

    [SerializeField] private GameObject[] levelSpecificObjects;

    [SerializeField] private string musicName;

    private void Start()
    {
        DeactivateNeccessaryObjectsAtTheBeginning();
        StartCoroutine(StartBlackOutSequence());
        AudioManager.Instance.PlayMusicClip(musicName);
    }

    private void DeactivateNeccessaryObjectsAtTheBeginning()
    {
        foreach (var obj in levelSpecificObjects)
        {
            obj.SetActive(false);
        }
        _repairGameWindow.SetActive(false);
        _repairZoneIndicator.SetActive(false);
        _endRepairGameButton.SetActive(false);
    }
    
    private IEnumerator StartBlackOutSequence()
    {
        yield return new WaitForSeconds(5); // Oyun başlatıldıktan 5 saniye sonra elektrikler kesilecek
        BlackOut();
    }

    private void BlackOut()
    {
        _isBlackedOut = true;
        _officeBG.GetComponent<SpriteRenderer>().color = new Color(0.27f, 0.27f, 0.27f, 1f); // Karartma

        OfficeCharacterController.OnBlackOut?.Invoke();
        CharacterMovement.OnBlackOut?.Invoke();
        
        _repairZoneIndicator.SetActive(true);

        foreach (var obj in levelSpecificObjects)
        {
            obj.SetActive(true);
        }
    }

    public void CheckElectricFixed()
    {
        if (_isBlackedOut)
        {
            EndBlackOut();
        }
    }

    private void EndBlackOut()
    {
        _isBlackedOut = false;
        _officeBG.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f); // Aydınlatma

        OfficeCharacterController.OnRepairComplete?.Invoke();
        CharacterMovement.OnEndBlackOut?.Invoke();

        StartCoroutine(TriggerWin());
    }

    public void StartRepairGame()
    {
        _endRepairGameButton.gameObject.SetActive(false);
        _repairGameWindow.SetActive(true);
        _repairZoneIndicator.SetActive(false); // Tamir bölgesini işaret eden objeyi gizle
    }

    public void FinishRepairGame()
    {
        _repairGameWindow.SetActive(false);
        CheckElectricFixed();
    }

    IEnumerator TriggerWin()
    {
        LevelManager.Instance.LevelCompleted(character);
        yield return new WaitForSeconds(2f);
        UIManager.Instance.ToggleWinMenu();
    }
}
