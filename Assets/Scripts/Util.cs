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
        return (WallType)
            (((int) type + 1) % WallType.GetValues(typeof(WallType)).Length);
    }

    public static Vector3 UnitVector (CardinalPoint point) {
        switch (point) {
            case CardinalPoint.north:
                return new Vector3(0, 0, -1);
            case CardinalPoint.east:
                return new Vector3(-1, 0, 0);
            case CardinalPoint.south:
                return new Vector3(0, 0, 1);
            case CardinalPoint.west:
                return new Vector3(1, 0, 0);

        }

        return Vector3.zero;
    }
}
