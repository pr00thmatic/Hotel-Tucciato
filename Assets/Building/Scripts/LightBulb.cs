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
        if (value) TurnOn();
        else TurnOff();
    }

    public void TurnOn () {
        isOn = true;

        lightSource.enabled = true;
        model.material = onMaterial;
    }

    public void TurnOff () {
        isOn = false;

        lightSource.enabled = false;
        model.material = offMaterial;
    }
}
