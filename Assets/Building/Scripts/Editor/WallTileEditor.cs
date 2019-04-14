using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Building {
    [CustomEditor(typeof(WallTile))]
    public class WallTileEditor : Editor {
        WallTile    _parsedTarget;
        WallTile    Target {
            get {
                if (_parsedTarget == null) _parsedTarget = (WallTile) target;
                return _parsedTarget;
            }
        }

        public override void OnInspectorGUI () {
            DrawDefaultInspector();
            Target.SetType(new WallTileInfo{
                    type = (WallType) EditorGUILayout.EnumPopup("Wall type: ", Target.Type),
                    doorAngle = Target.door.rotation.eulerAngles.y
                });

            GUILayout.Label("Door angle: " + Target.DoorAngle);

            if (GUI.changed) {
                EditorUtility.SetDirty(Target);
                EditorSceneManager.MarkSceneDirty(Target.gameObject.scene);
            }
        }
    }
}
