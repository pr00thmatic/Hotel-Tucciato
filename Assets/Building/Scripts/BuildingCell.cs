using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Building {
    public class BuildingCell : MonoBehaviour {
        public BuildingTile tile;
        public MatrixBuilding MatrixBuilding {
            get => Util.FindInParent<MatrixBuilding>(transform);
        }
        public Coord Coord {
            get => Coord.FromWorld(transform.localPosition, FloorTile.tileSize);
        }

        public void Initialize () {
            tile = GetComponent<BuildingTile>();
        }

        public void RemoveNeighbour (CardinalPoint location) {
            MatrixBuilding building = Util.FindInParent<MatrixBuilding>(transform);
            building.Remove(Util.GetCoord(location, transform));
        }


        public void AddNeighbour (CardinalPoint location) {
            MatrixBuilding building = Util.FindInParent<MatrixBuilding>(transform);
            building.Add(Coord.Cast(Util.UnitVector(location)) +
                         Coord.FromWorld(transform.localPosition,
                                         FloorTile.tileSize));
        }

        public BuildingCellState GetState () {
            if (tile == null) Initialize();

            return new BuildingCellState() {
                tile = tile.GetState(),
                coord = this.Coord
            };
        }
    }
}
