using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Building {
    [System.Serializable]
    public class BuildingTileState {
        public WallTileState[] walls = new WallTileState[4];
        public LightBulbState ceilingLight = new LightBulbState();

        public BuildingTileState () {
            for (int i=0; i<4; i++) {
                walls[i] = new WallTileState();
            }

            walls[(int) CardinalPoint.south].type =
                walls[(int) CardinalPoint.north].type =
                WallType.simple;
        }
    }
}
