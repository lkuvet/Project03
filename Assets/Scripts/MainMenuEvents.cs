using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class MainMenuEvents : MonoBehaviour
{
    private UIDocument _document;
    private Button _playButton;
    private Button _quitButton;

    private void Awake()
    {
        _document = GetComponent<UIDocument>();
        _playButton = _document.rootVisualElement.Q("PlayButton") as Button;
        _playButton.RegisterCallback<ClickEvent>(OnPlayButtonClicked);
        _quitButton = _document.rootVisualElement.Q("QuitButton") as Button;
        _quitButton.RegisterCallback<ClickEvent>(OnQuitButtonClicked);
    }

    private void OnPlayButtonClicked(ClickEvent evt)
    {
        SceneManager.LoadScene("GamePlay");
    }

    private void OnQuitButtonClicked(ClickEvent evt)
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }

    private void OnDisable()
    {
        _playButton.UnregisterCallback<ClickEvent>(OnPlayButtonClicked);
        _quitButton.UnregisterCallback<ClickEvent>(OnQuitButtonClicked);
    }
}
