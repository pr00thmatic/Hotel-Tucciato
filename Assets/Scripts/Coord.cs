using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Coord {
    public int x;
    public int z;

    public Coord (int x, int z) { this.x = x; this.z = z; }

    public static Coord FromWorld (Vector3 pos, float tileSize) {
        return new Coord((int) (pos.x/tileSize),
                         (int) (pos.z/tileSize));
    }

    public Vector3 ToWorld (float tileSize) {
        return this * new Vector3(1, 0, 1) * tileSize;
    }

    public static Vector3 operator * (Coord a, Vector3 v) {
        return new Vector3(v.x * a.x, v.y, v.z * a.z);
    }

    public override string ToString () {
        return "(" + x + "," + z + ")";
    }
}
