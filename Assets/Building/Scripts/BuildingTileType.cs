using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Building {
    [System.Serializable]
    public class BuildingTileType {
        public Dictionary<CardinalPoint, WallType> typeOfWall =
            new Dictionary<CardinalPoint, WallType>() {

            { CardinalPoint.north, WallType.none },
            { CardinalPoint.west, WallType.none },
            { CardinalPoint.east, WallType.none },
            { CardinalPoint.south, WallType.none }

        };
    }
}
