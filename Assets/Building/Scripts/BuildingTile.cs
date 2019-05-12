using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Building {
    public class BuildingTile : MonoBehaviour {
        public LightBulb ceilingLight;

        public void ToggleCeilingLightExistence () {
            ceilingLight.SetActive(!ceilingLight.isActive);
        }

        public void ToggleCeilingLight () {
            ceilingLight.Toggle();
        }

        public void ShuffleWall (CardinalPoint wallPos) {
            WallTile wall = transform.Find("walls/" + wallPos).GetComponent<WallTile>();
            wall.Shuffle();
        }

        public void SetState (BuildingTileState state) {
            foreach (Transform child in transform.Find("walls")) {
                WallTile wall = child.GetComponent<WallTile>();
                CardinalPoint wallPos = (CardinalPoint) CardinalPoint.
                    Parse(typeof(CardinalPoint), wall.name);
                wall.SetState(state.walls[(int) wallPos]);
            }

            ceilingLight.SetActive(state.ceilingLight.exists);
            ceilingLight.Toggle(state.ceilingLight.isOn);
        }

        public BuildingTileState GetState () {
            BuildingTileState state = new BuildingTileState();

            foreach (Transform child in transform.Find("walls")) {
                WallTile wall = child.GetComponent<WallTile>();
                CardinalPoint wallPos = (CardinalPoint) CardinalPoint.
                    Parse(typeof(CardinalPoint), wall.name);
                state.walls[(int) wallPos] = wall.GetState();
            }

            state.ceilingLight.exists = ceilingLight.isActive;
            state.ceilingLight.isOn = ceilingLight.isOn;

            return state;
        }
    }
}
