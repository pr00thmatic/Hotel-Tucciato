using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Building {
    public class BuildingTile : MonoBehaviour {
        public BuildingTileType _currentType = new BuildingTileType();
        public BuildingTileType CurrentType {
            get => _currentType;
            set => SetBuildingTileType(value);
        }

        public void SetBuildingTileType (BuildingTileType type) {
            _currentType = type;
            UpdateBuildingType();
        }

        public void UpdateBuildingType () {
            foreach (Transform wall in transform.Find("walls")) {
                WallTile wt = wall.GetComponent<WallTile>();
                wt.SetType(CurrentType.typeOfWall[wall.name]);
            }
        }
    }
}
