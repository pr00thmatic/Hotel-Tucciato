using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Building {
    [CustomEditor(typeof(WallTile))]
    public class WallTileEditor : GenericEditor<WallTile> {
        public override void CustomInspectorGUI () {
            GUILayout.Label("Door angle: " + Target.DoorAngle);
        }
    }
}
