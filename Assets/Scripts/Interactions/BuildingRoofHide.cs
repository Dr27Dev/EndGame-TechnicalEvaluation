using System;
using DG.Tweening;
using UnityEngine;

public class BuildingRoofHide : MonoBehaviour
{
    [SerializeField] private MeshRenderer[] _roofMeshes;
    [SerializeField] private float _transitionDuration = 1;
    private Material[] _roofMaterials;
    private void Awake()
    {
        int matsIndex = 0;
        _roofMaterials = new Material[_roofMeshes.Length];
        foreach (var mesh in _roofMeshes)
        {
            _roofMaterials[matsIndex] = mesh.material;
            matsIndex++;
        }

        foreach (var mat in _roofMaterials) mat.SetFloat("_Alpha", 1);
    }

    private void SetRoofOpacity(float value)
    {
        foreach (var mat in _roofMaterials)
        {
            DOTween.To(() => mat.GetFloat("_Alpha"),
                x => mat.SetFloat("_Alpha", x), value, _transitionDuration);
        }
    }

    private void OnTriggerEnter(Collider other) { if (other.CompareTag("Player")) SetRoofOpacity(0); }
    private void OnTriggerExit(Collider other) { if (other.CompareTag("Player")) SetRoofOpacity(1); }
}
