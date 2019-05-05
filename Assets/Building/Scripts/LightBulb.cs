using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LightBulb : MonoBehaviour {
    public Light lightSource;
    public Renderer model;
    public Material onMaterial;
    public Material offMaterial;
    public bool isOn = true;
    public bool isActive = true;

    public void SetActive (bool value) {
        isActive = value;
        model.gameObject.SetActive(value);
        lightSource.gameObject.SetActive(value);
    }

    public void Toggle () {
        Toggle(!isOn);
    }

    public void Toggle (bool value) {
        isOn = value;
        lightSource.enabled = value;

        if (isOn) {
            model.material = onMaterial;
        } else {
            model.material = offMaterial;
        }
    }
}
