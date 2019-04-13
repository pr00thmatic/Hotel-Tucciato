using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Building {
    public class WallTile : MonoBehaviour {
        public WallType Type { get => _type; }
        WallType _type;

        public void SetType (WallType type) {
            this._type = type;

            foreach (Transform wallTypeInstance in transform) {
                wallTypeInstance.gameObject.
                    SetActive(type.ToString() == wallTypeInstance.name);
            }
        }
    }
}
