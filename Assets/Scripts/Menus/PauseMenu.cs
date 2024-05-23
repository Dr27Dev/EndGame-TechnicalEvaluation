using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _exitButton;
    private bool paused;

    private void Start()
    {
        _pauseMenu.SetActive(false);
        if (GameManager.Instance.IsMobile) _exitButton.SetActive(false);
        GameManager.Instance.Player.GetComponent<PlayerStandaloneInput>().Input_Pause += TogglePause;
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
        paused = true;
    }

    public void Resume()
    {
        Time.timeScale = 1;
        _pauseMenu.SetActive(false);
        paused = false;
    }
}
