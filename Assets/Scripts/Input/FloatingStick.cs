using System;
using UnityEngine;

public class FloatingStick : MonoBehaviour
{
    [HideInInspector] public RectTransform RectTransform;
    public RectTransform Knob;
    private void Awake() => RectTransform = GetComponent<RectTransform>();
}
