using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Building {
    [System.Serializable]
    public class BuildingTileType {
        public WallType[] typeOfWall = {
            WallType.simple,
            WallType.none,
            WallType.none,
            WallType.none
        };
    }
}
