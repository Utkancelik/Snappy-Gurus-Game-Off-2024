using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Buttons")] 
    [SerializeField] private Button _mainMenuButton;
    [SerializeField] private Button _pauseButton;

    private void Start()
    {
        _mainMenuButton.onClick.AddListener(LoadMainMenu);
    }

    private void LoadMainMenu()
    {
        SceneManager.LoadScene((int)SceneIndex.MainMenu);
    }
}
