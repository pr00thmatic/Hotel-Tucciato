using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Building {
    public class WallTile : MonoBehaviour {
        public Transform door;
        public WallType Type { get => _type; }
        public float DoorAngle {
            get => door.localRotation.eulerAngles.y;
            set => door.localRotation = Quaternion.Euler(-90, value, 0);
        }
        WallType _type;

        public void SetType (WallTileInfo info) {
            _type = info.type;
            foreach (Transform wallTypeInstance in transform) {
                wallTypeInstance.gameObject.
                    SetActive(info.type.ToString() == wallTypeInstance.name);
            }

            DoorAngle = info.doorAngle;
        }
    }
}
