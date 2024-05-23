using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _exitButton;
    [SerializeField] private GameObject _controlsScreen;
    [SerializeField] private GameObject _controlsMobile;
    [SerializeField] private GameObject _controlsDesktop;
    private bool paused;

    private void Start()
    {
        _pauseMenu.SetActive(false);
        if (GameManager.Instance.IsMobile) _exitButton.SetActive(false);
        GameManager.Instance.Player.GetComponent<PlayerStandaloneInput>().Input_Pause += TogglePause;
        _controlsScreen.SetActive(false);
        _controlsMobile.SetActive(false);
        _controlsDesktop.SetActive(false);
    }

    private void TogglePause()
    {
        if (paused) Resume();
        else Pause();
    }

    public void Pause()
    {
        Time.timeScale = 0;
        _pauseMenu.SetActive(true);
        _controlsScreen.SetActive(false);
        paused = true;
        if (GameManager.Instance.IsMobile)
            GameManager.Instance.Player.GetComponent<PlayerMobileInput>().enabled = false;
    }

    public void Resume()
    {
        Time.timeScale = 1;
        _pauseMenu.SetActive(false);
        _controlsScreen.SetActive(false);
        paused = false;
        if (GameManager.Instance.IsMobile)
            GameManager.Instance.Player.GetComponent<PlayerMobileInput>().enabled = true;
    }
    
    public void Controls()
    {
        _controlsScreen.SetActive(true);
        if (GameManager.Instance.IsMobile) _controlsMobile.SetActive(true);
        else _controlsDesktop.SetActive(true);
    }

    public void Exit() => Application.Quit();
}
