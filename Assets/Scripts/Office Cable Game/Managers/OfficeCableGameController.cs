using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OfficeCableGameController : MonoBehaviour
{
    [SerializeField] private GameObject _officeBG; // karartılacak sonra aydınlatılacak olan background resmi
    [SerializeField] private GameObject _repairGameWindow;
    [SerializeField] private GameObject _repairZoneIndicator; // Tamir bölgesini işaret edecek obje
    [SerializeField] public GameObject _endRepairGameButton;
    
    private bool _isBlackedOut = false;

    private void Start()
    {
        DeactivateNeccessaryObjectsAtTheBeginning();
        StartCoroutine(StartBlackOutSequence());
    }

    private void DeactivateNeccessaryObjectsAtTheBeginning()
    {
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

        _repairZoneIndicator.SetActive(true); // Tamir bölgesini işaret et
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
}