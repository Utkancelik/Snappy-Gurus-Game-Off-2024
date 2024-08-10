using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Buttons")] 
    [SerializeField] private Button _mainMenuButton;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _continueButton;
    [SerializeField] private GameObject _continueButtonObject;

    [Header("Panels")]
    [SerializeField] private GameObject _pauseMenuPanel;
    
    [SerializeField] private TMP_Text _pauseMenuPanelHeader;
    private bool isPauseMenuActive;
    
    public static UIManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    
    private void Start()
    {
        Time.timeScale = 1;
        _pauseMenuPanel.SetActive(false);
        _mainMenuButton.onClick.AddListener(LoadMainMenu);
        _restartButton.onClick.AddListener(RestartScene);
        _continueButton.onClick.AddListener(TogglePauseMenu);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }
    }

    private void LoadMainMenu()
    {
        SceneManager.LoadScene((int)SceneIndex.MainMenu);
        Time.timeScale = 1;
    }

    private void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void TogglePauseMenu()
    {
        if (_pauseMenuPanel.activeInHierarchy)
        {
            _pauseMenuPanelHeader.text = "PAUSE";
            _continueButtonObject.SetActive(true);
            _pauseMenuPanel.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 0;
            _pauseMenuPanel.SetActive(true);
        }
    }

    public void ToggleWinMenu()
    {
        AudioManager.Instance.PlayMusicClip("Win");
        Time.timeScale = 0;
        _pauseMenuPanelHeader.text = "YOU WIN!";
        _pauseMenuPanel.SetActive(true);
        _continueButtonObject.SetActive(false);
    }
    
    public void ToggleLoseMenu()
    {
        AudioManager.Instance.PlayMusicClip("Lost");
        Time.timeScale = 0;
        _pauseMenuPanelHeader.text = "YOU LOST!";
        _pauseMenuPanel.SetActive(true);
        _continueButtonObject.SetActive(false);
    }
}
