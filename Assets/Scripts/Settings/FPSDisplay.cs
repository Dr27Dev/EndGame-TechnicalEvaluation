using UnityEngine;
using TMPro;

public class FPSDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _fps_Text;
    private float deltaTime = 0.0f;

    private void Start()
    {
        Application.targetFrameRate = GameManager.Instance.IsMobile ? 60 : 144;
    }

    void Update()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        _fps_Text.text = string.Format("{0:0.}\nFPS", fps);
    }
}