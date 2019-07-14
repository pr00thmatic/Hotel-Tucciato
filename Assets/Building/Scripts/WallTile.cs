using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Building {
    public class WallTile : MonoBehaviour {
        public Transform models;
        public Transform door;
        public float DoorAngle {
            get => door.localRotation.eulerAngles.y;
            set => door.localRotation = Quaternion.Euler(-90, value, 0);
        }
        public WallType CurrentType {
            get => _activeWall == null? WallType.none:
                (WallType) Enum.Parse(typeof(WallType), _activeWall.name);
        }
        GameObject _activeWall;

        public void Shuffle () {
            SetType(Util.Next(CurrentType));
        }

        public void SetType (WallType type) {
            _activeWall = null;
            foreach (Transform child in models) {
                child.gameObject.SetActive(type.ToString() == child.name);
                if (child.gameObject.activeSelf) {
                    _activeWall = child.gameObject;
                }
            }
        }

        public void SetState (WallTileState state) {
            SetType(state.type);
            DoorAngle = state.doorAngle;
        }

        public WallTileState GetState () {
            WallTileState state = new WallTileState();
            foreach (Transform wallTypeInstance in models) {
                if (wallTypeInstance.gameObject.activeSelf) {
                    state.type = (WallType)
                        Enum.Parse(typeof(WallType), wallTypeInstance.name);
                    break;
                }
            }

            state.doorAngle = DoorAngle;
            return state;
        }
    }
}
