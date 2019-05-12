using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Building {
    public class BuildingCell : MonoBehaviour {
        public List<BuildingCell> connected;
        public BuildingTile tile;
        public bool IsPossible {
            get {
                foreach (CardinalPoint direction in Enum.GetValues(typeof(CardinalPoint))) {
                    Vector3 unitVector = Util.UnitVector(direction);
                    CardinalPoint opposite = Util.Direction(unitVector * -1);
                    WallTile connectedWall = connected[(int) direction].tile.
                        GetWall(opposite);

                    if (connectedWall.CurrentType == WallType.door ||
                        connectedWall.CurrentType == WallType.none) {
                        return true;
                    }
                }

                return false;
            }
        }

        public void Initialize () {
            tile = GetComponent<BuildingTile>();
        }
    }
}
