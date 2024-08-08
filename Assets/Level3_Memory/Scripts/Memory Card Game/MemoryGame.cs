using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MemoryGame : MonoBehaviour
{
    public GameObject cardPrefab;
    public RectTransform cardPanel;
    public Sprite cardBack;
    public List<Sprite> cardImages;
    public RectTransform tablePanel;
    public GameObject tableSlotPrefab;

    private List<GameObject> cards = new List<GameObject>();
    private List<Sprite> gameImages = new List<Sprite>();
    private List<Button> selectedCards = new List<Button>();
    private List<RectTransform> tableSlots = new List<RectTransform>();
    private bool canClick = true;
    private int filledSlotsCount = 0;

    void Start()
    {
        cardPanel.gameObject.SetActive(false);
        tablePanel.gameObject.SetActive(false);
    }

    public void StartGame()
    {
        InitializeCards();
        InitializeTableSlots();
        cardPanel.gameObject.SetActive(true);
        tablePanel.gameObject.SetActive(true);
    }

    void InitializeCards()
    {
        foreach (Sprite image in cardImages)
        {
            gameImages.Add(image);
            gameImages.Add(image);
        }

        gameImages = ShuffleList(gameImages);

        for (int i = 0; i < gameImages.Count; i++)
        {
            GameObject newCard = Instantiate(cardPrefab, cardPanel);
            Button cardButton = newCard.GetComponent<Button>();
            Image cardImage = newCard.transform.GetChild(0).GetComponent<Image>();
            cardImage.sprite = cardBack;
            int index = i;
            cardButton.onClick.AddListener(() => OnCardClick(cardButton, gameImages[index]));
            cards.Add(newCard);
        }
    }

    void InitializeTableSlots()
    {
        int slotCount = cardImages.Count;
        for (int i = 0; i < slotCount; i++)
        {
            GameObject newSlot = Instantiate(tableSlotPrefab, tablePanel);
            RectTransform slotRectTransform = newSlot.GetComponent<RectTransform>();
            slotRectTransform.localScale = Vector3.one;
            slotRectTransform.anchoredPosition = Vector2.zero;
            slotRectTransform.sizeDelta = new Vector2(100, 100);
            tableSlots.Add(slotRectTransform);
        }
    }

    void OnCardClick(Button card, Sprite image)
    {
        if (!canClick || selectedCards.Contains(card)) return;

        card.transform.GetChild(0).GetComponent<Image>().sprite = image;
        selectedCards.Add(card);

        if (selectedCards.Count == 2)
        {
            canClick = false;
            StartCoroutine(CheckMatch());
        }
    }

    IEnumerator CheckMatch()
    {
        yield return new WaitForSeconds(1);

        if (selectedCards[0].transform.GetChild(0).GetComponent<Image>().sprite == selectedCards[1].transform.GetChild(0).GetComponent<Image>().sprite)
        {
            Sprite matchedSprite = selectedCards[0].transform.GetChild(0).GetComponent<Image>().sprite;
            foreach (Button card in selectedCards)
            {
                cards.Remove(card.gameObject);
                Destroy(card.gameObject);
            }
            PlaceMatchedCardOnTable(matchedSprite);
        }
        else
        {
            foreach (Button card in selectedCards)
            {
                card.transform.GetChild(0).GetComponent<Image>().sprite = cardBack;
            }
        }

        selectedCards.Clear();
        canClick = true;
    }

    List<T> ShuffleList<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            T temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
        return list;
    }

    void PlaceMatchedCardOnTable(Sprite matchedSprite)
    {
        foreach (RectTransform slot in tableSlots)
        {
            if (slot.childCount == 0)
            {
                GameObject newImage = new GameObject("MatchedCard");
                newImage.transform.SetParent(slot);
                RectTransform imageRectTransform = newImage.AddComponent<RectTransform>();
                imageRectTransform.localScale = Vector3.one;
                imageRectTransform.anchoredPosition = Vector2.zero;
                imageRectTransform.sizeDelta = slot.sizeDelta;
                Image imageComponent = newImage.AddComponent<Image>();
                imageComponent.sprite = matchedSprite;
                filledSlotsCount++;
                CheckGameCompletion();
                break;
            }
        }
    }

    void CheckGameCompletion()
    {
        if (filledSlotsCount == tableSlots.Count)
        {
            // Oyunun kazanıldığını bildir
            Debug.Log("Tebrikler! Oyunu kazandınız!");

            // Alex'in kızgınlığını artır
            EmotionController.Instance.UpdatePlayerPrefs(EmotionController.Character.Alex, EmotionController.EmotionState.Anger);
        }
    }


}
