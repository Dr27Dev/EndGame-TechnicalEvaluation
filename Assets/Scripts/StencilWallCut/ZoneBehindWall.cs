using System;
using UnityEngine;
public class ZoneBehindWall : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) => other.GetComponent<StencilMask>().EnableMask();
    private void OnTriggerExit(Collider other) => other.GetComponent<StencilMask>().DisableMask();
}
