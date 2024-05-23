using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool IsMobile;
    public GameObject Player;

    private void Awake()
    {
        Instance = this;
        if (Player == null) Player = FindObjectOfType<PlayerController>().GameObject();
    }
}
