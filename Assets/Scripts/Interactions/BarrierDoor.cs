using DG.Tweening;
using UnityEngine;

public class BarrierDoor : MonoBehaviour
{
    [SerializeField] private ParticleSystem _doorClosedVFX;
    [SerializeField] private ParticleSystem _openDoorVFX;
    [SerializeField] private MeshRenderer _keylock;
    [SerializeField] private float _transitionDuration = 0.5f;
    [SerializeField] private Collider _barrierCollider;
    [SerializeField] private int _doorCode = 0;
    
    private Material _keylockMat;
    private bool _isDoorOpen;

    private void Awake()
    {
        _keylockMat = _keylock.material;
        _doorClosedVFX.Play();
        SetDoorOpacity(1);
        _barrierCollider.enabled = true;
        _isDoorOpen = false;
    }

    private void OpenDoor()
    {
        _doorClosedVFX.Stop(true);
        _doorClosedVFX.Clear(true);
        _openDoorVFX.Play();
        SetDoorOpacity(0);
        _barrierCollider.enabled = false;
        _isDoorOpen = true;
    }
    
    private void SetDoorOpacity(float value)
    {
        DOTween.To(() => _keylockMat.GetFloat("_Alpha"),
            x => _keylockMat.SetFloat("_Alpha", x), value, _transitionDuration);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !_isDoorOpen)
        {
            if (other.GetComponent<PlayerInventory>().CheckHasKey(_doorCode))
                OpenDoor();
        }
    }
}
