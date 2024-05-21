using System;
using DG.Tweening;
using UnityEngine;

public class BarrierDoor : MonoBehaviour
{
    [SerializeField] private ParticleSystem _doorClosedVFX;
    [SerializeField] private ParticleSystem _openDoorVFX;
    [SerializeField] private MeshRenderer _keylock;
    [SerializeField] private float _transitionDuration = 0.5f;
    private Material _keylockMat;

    private void Awake()
    {
        _keylockMat = _keylock.material;
        _doorClosedVFX.Play();
        SetDoorOpacity(1);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) OpenDoor();
    }

    private void OpenDoor()
    {
        _doorClosedVFX.Stop(true);
        _doorClosedVFX.Clear(true);
        _openDoorVFX.Play();
        SetDoorOpacity(0);
    }
    
    private void SetDoorOpacity(float value)
    {
        DOTween.To(() => _keylockMat.GetFloat("_Alpha"),
            x => _keylockMat.SetFloat("_Alpha", x), value, _transitionDuration);
    }
}
