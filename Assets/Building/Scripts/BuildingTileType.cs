using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Building {
    [System.Serializable]
    public class BuildingTileType {
        public WallTileInfo[] walls = new WallTileInfo[4];

        public BuildingTileType () {
            for (int i=0; i<4; i++) {
                walls[i] = new WallTileInfo();
            }

            walls[(int) CardinalPoint.south].type =
                walls[(int) CardinalPoint.north].type =
                WallType.simple;
        }
    }
}
