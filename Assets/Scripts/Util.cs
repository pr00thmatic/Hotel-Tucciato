using UnityEngine;
using UnityEditor;
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

    public static GameObject Instantiate (GameObject thingie, string message = "") {
        #if UNITY_EDITOR
        GameObject created;
        if (!Application.isPlaying) {
            created = PrefabUtility.InstantiatePrefab(thingie) as GameObject;
        } else {
            created = GameObject.Instantiate(thingie);
        }
        Undo.RegisterCreatedObjectUndo(created, message == ""?
                                       "created thingie " + thingie.name: message);
        #else
        created = GameObject.Instantiate(thingie);
        #endif

        return created;
    }

    public static void SafeDestroy (GameObject thingie) {
        #if UNITY_EDITOR
        if (Application.isPlaying) {
            GameObject.Destroy(thingie);
        } else {
            Undo.DestroyObjectImmediate(thingie);
        }
        #else
        GameObject.Destroy(thingie);
        #endif
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

    public static Coord GetCoord (CardinalPoint orientation, Transform reference) {
        return Coord.FromWorld(reference.localPosition, FloorTile.tileSize) +
            Coord.Cast(Util.UnitVector(orientation));
    }
}
