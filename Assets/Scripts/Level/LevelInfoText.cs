using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class LevelInfoText : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject _textInfo;
    [SerializeField] private TMP_Text _episodeNumberText;
    [SerializeField] private RectTransform imageTransform; 
    
    private Vector3 originalScale;  
    private Vector3 hoverScale;     

    private LevelButton _levelButton;
    private void Start()
    {
        _textInfo = transform.GetChild(0).gameObject;
        _levelButton = GetComponent<LevelButton>();
        imageTransform = GetComponent<RectTransform>();
        _episodeNumberText = _textInfo.transform.GetChild(0).GetComponent<TMP_Text>();
        _textInfo.SetActive(false);
        
        originalScale = imageTransform.localScale;
        hoverScale = originalScale * 1.1f; // Boyutu %10 artır
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_textInfo != null)
        {
            string levelNumber = _levelButton.levelIndex.ToString();
            string newLevelNumber = levelNumber.Substring(5,1);
            levelNumber = newLevelNumber;
            
            _episodeNumberText.text = $"Episode {levelNumber}";
            _textInfo.SetActive(true);
        }
        imageTransform.localScale = hoverScale;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_textInfo != null)
        {
            _textInfo.SetActive(false);
        }
        imageTransform.localScale = originalScale;
    }
}