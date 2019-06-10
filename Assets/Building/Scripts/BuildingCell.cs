using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Building {
    public class BuildingCell : MonoBehaviour {
        public BuildingCell[] connected = new BuildingCell[4];
        public BuildingTile tile;

        public void Initialize () {
            tile = GetComponent<BuildingTile>();
            connected = new BuildingCell[4];
        }

        public void RemoveNeighbour (CardinalPoint location) {
            BuildingCell neighbour = connected[(int) location];
            if (neighbour == null) return;

            try {
                Util.FindInParent<MatrixBuilding>(transform)
                    .pieces[Coord.FromWorld(neighbour.gameObject.transform.position,
                                            FloorTile.tileSize)] = null;
            } catch {
                Destroy(neighbour.gameObject);
                Util.FindInParent<MatrixBuilding>(transform).PopulatePiecesInfo();
            }
            Util.SafeDestroy(neighbour.gameObject);
        }

        public void ReadNeighbours () {
            // a building cell is always children of a MatrixBuilding
            MatrixBuilding matrix = Util.FindInParent<MatrixBuilding>(transform);
            matrix.PopulatePiecesInfo();
            Coord selfCoord = Coord.FromWorld(transform.position, FloorTile.tileSize);

            for (int xStep=-1; xStep<=1; xStep++) {
                for (int zStep=-1; zStep<=1; zStep++) {
                    if (xStep == zStep) continue;

                    Coord neighbour = new Coord(selfCoord.x + xStep,
                                                selfCoord.z + zStep);

                    if (matrix.pieces.ContainsKey(neighbour) == false) continue;

                    connected[(int) Util.Direction(new Vector3(xStep, 0, zStep))] =
                        matrix.pieces[neighbour];
                }
            }
        }

        public void AddNeighbour (CardinalPoint location) {
            BuildingCell neighbour = Instantiate(this.gameObject).
                GetComponent<BuildingCell>();
            connected[(int) location] = neighbour;
            neighbour.connected[(int) Util.Opposite(location)] = this;

            neighbour.transform.parent = transform.parent;
            neighbour.transform.localPosition = transform.localPosition +
                Util.UnitVector(location) * FloorTile.tileSize;

            Debug.Log("adding a neighbour at " + location + " name is " + neighbour.name);
        }

        public BuildingCellState GetState () {
            if (tile == null) Initialize();

            return new BuildingCellState() {
                tile = tile.GetState(),
                coord = Coord.FromWorld(transform.position, FloorTile.tileSize)
            };
        }
    }
}
