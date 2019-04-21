using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Building {
    [System.Serializable]
    public class WallTileState {
        public WallType type = WallType.none;
        public float doorAngle = 0;
    }
}
