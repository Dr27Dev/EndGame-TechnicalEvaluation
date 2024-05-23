using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    [SerializeField] private GameObject _deathScreen;

    private void Start()
    {
        _deathScreen.SetActive(false);
        Time.timeScale = 1;
    }

    public void Die()
    {
        _deathScreen.SetActive(true);
        Time.timeScale = 0;
    }
    
    public void Respawn() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);
}
