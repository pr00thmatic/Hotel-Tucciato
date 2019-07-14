using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Rotate : MonoBehaviour {
    public float speed = 45;

    void Update () {
        transform.Rotate(0, speed * Time.deltaTime, 0);
    }
}
