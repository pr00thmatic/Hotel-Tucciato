using UnityEngine;
using UnityEngine.Rendering;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class Shyness : MonoBehaviour {
    public Transform target;
    public Renderer[] cachedRenderers;
    public bool shy;
    public Renderer visibilityProbe;

    void Awake () {
        if (cachedRenderers == null || cachedRenderers.Length == 0) {
            cachedRenderers = target.GetComponentsInChildren<Renderer>();
        }
    }

    void Update () {
        if (!visibilityProbe.isVisible) return;
        Vector3 view = Camera.main.transform.position - transform.position;
        float difference = Vector3.SignedAngle(view, target.forward, Vector3.up);
        bool isShy = difference < -90 || difference > 90;
        shy = isShy;

        foreach (Renderer r in cachedRenderers) {
            if (!r.gameObject.activeInHierarchy) continue;

            if (!isShy || Util.FindInParent<NotShy>(r.transform) != null) {
                r.shadowCastingMode = ShadowCastingMode.On;
            } else if (isShy) {
                r.shadowCastingMode = ShadowCastingMode.ShadowsOnly;
            }
        }
    }
}
