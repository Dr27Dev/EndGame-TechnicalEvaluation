using System;
using DG.Tweening;
using UnityEngine;
public class StencilMask : MonoBehaviour
{
    [SerializeField] private float _maxScale;
    [SerializeField] private float _transitionTime;
    [SerializeField] private Transform _maskMesh;

    private void Start() => SetMaskScale(0, 0);
    public void EnableMask() => SetMaskScale(_maxScale, _transitionTime);
    public void DisableMask() => SetMaskScale(0, _transitionTime);
    private void SetMaskScale(float scale, float time)
    {
        _maskMesh.DOComplete();
        _maskMesh.transform.DOScale(new Vector3(scale, scale, scale), time);
    }
}
