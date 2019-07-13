using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Building; // ToWorld(): Vector3

[System.Serializable]
public struct Coord {
    public int x;
    public int z;

    public Coord (int x, int z) { this.x = x; this.z = z; }

    public static Coord FromWorld (Vector3 pos, float tileSize) {
        return new Coord((int) (pos.x/tileSize),
                         (int) (pos.z/tileSize));
    }

    public static Coord Cast (Vector3 raw) {
        return new Coord((int) raw.x, (int) raw.z);
    }

    public Vector3 ToWorld () {
        return ToWorld(FloorTile.tileSize);
    }

    public Vector3 ToWorld (float tileSize) {
        return this * new Vector3(1, 0, 1) * tileSize;
    }

    public static Coord operator + (Coord a, Coord b) {
        return new Coord(a.x + b.x, a.z + b.z);
    }

    public static Vector3 operator * (Coord a, Vector3 v) {
        return new Vector3(v.x * a.x, v.y, v.z * a.z);
    }

    public static Coord operator + (Coord a, CardinalPoint direction) {
        Vector3 v = Util.UnitVector(direction);
        return a + Cast(v);
    }

    public override string ToString () {
        return "(" + x + "," + z + ")";
    }
}
