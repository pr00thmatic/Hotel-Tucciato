using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Building {
    public class BuildingTile : MonoBehaviour {
        public BuildingTileType _currentType = new BuildingTileType();
        public BuildingTileType CurrentType {
            get {
                UpdateState();
                return _currentType;
            }
            set => SetBuildingTileType(value);
        }

        public void UpdateState () {
            foreach (Transform wall in transform.Find("walls")) {
                WallTile wt = wall.GetComponent<WallTile>();
                CardinalPoint orientation = (CardinalPoint)
                    CardinalPoint.Parse(typeof(CardinalPoint), wt.name);
                _currentType.walls[(int) orientation].doorAngle = wt.DoorAngle;
            }
        }

        public void SetBuildingTileType (BuildingTileType type) {
            _currentType = type;
            UpdateBuildingType();
        }

        public void UpdateBuildingType () {
            foreach (Transform wall in transform.Find("walls")) {
                WallTile wt = wall.GetComponent<WallTile>();
                CardinalPoint orientation = (CardinalPoint)
                    System.Enum.Parse(typeof(CardinalPoint), wall.name);
                wt.SetType(_currentType.walls[(int) orientation]);
            }
        }
    }
}
