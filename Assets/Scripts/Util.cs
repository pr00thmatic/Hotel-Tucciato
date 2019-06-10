using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Building;

[System.Serializable]
public class Util {
    public static Vector3[] unitVectors = new Vector3[] {
        new Vector3(0, 0, -1), new Vector3(-1, 0, 0),
        new Vector3(0, 0, 1), new Vector3(1, 0, 0)
    };

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

    public static CardinalPoint Direction (Vector3 unitVector) {
        foreach (CardinalPoint direction in Enum.GetValues(typeof(CardinalPoint))) {
            if (unitVector == unitVectors[(int) direction]) return direction;
        }

        return CardinalPoint.north;
    }

    public static CardinalPoint Opposite (CardinalPoint point) {
        return Direction(UnitVector(point) * -1);
    }

    public static Vector3 UnitVector (CardinalPoint point) {
        return unitVectors[(int) point];
    }

    public static CardinalPoint[] ListCardinalPoints () {
        return Enum.GetValues(typeof(CardinalPoint)) as CardinalPoint[];
    }

    public static T FindInParent<T> (Transform parent) where T: MonoBehaviour {
        T found = null;

        do {
            found = parent.GetComponent<T>();
            if (found != null) {
                return found;
            }
            parent = parent.parent;
        } while (parent != null);

        return found;
    }
}
