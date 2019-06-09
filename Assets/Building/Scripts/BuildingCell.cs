using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Building {
    public class BuildingCell : MonoBehaviour {
        public BuildingCell[] connected;
        public BuildingTile tile;

        public void Initialize () {
            tile = GetComponent<BuildingTile>();
            connected = new BuildingCell[4];
        }

        public void AddNeighbour (CardinalPoint location) {
            BuildingCell neighbour = Instantiate(this.gameObject).
                GetComponent<BuildingCell>();
            connected[(int) location] = neighbour;
            neighbour.connected[(int) Util.Opposite(location)] = this;
            neighbour.transform.localPosition = transform.localPosition +
                Util.UnitVector(location) * FloorTile.tileSize;
            neighbour.transform.parent = transform.parent;
            Debug.Log("adding a neighbour at " + location + " name is " + neighbour.name);
        }
    }
}
