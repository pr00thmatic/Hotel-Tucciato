using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Building;

[System.Serializable]
public class Util {
    public static void SafeDestroy (GameObject thingie) {
        if (Application.isPlaying) {
            GameObject.Destroy(thingie);
        } else {
            GameObject.DestroyImmediate(thingie);
        }
    }

    public static WallType Next (WallType type) {
        int index = (int) type;
        if (index >= 100) {
            index = 0;
        } else {
            index++;
            index %= WallType.GetValues(typeof(WallType)).Length-1;
        }

        return (WallType) index;
    }
}
