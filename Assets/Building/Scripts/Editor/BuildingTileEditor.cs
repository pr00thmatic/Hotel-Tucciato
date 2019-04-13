using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Building {
    [CustomEditor(typeof(BuildingTile))]
    public class BuildingTileEditor : Editor {
        BuildingTile    _parsedTarget;
        BuildingTile    Target {
            get {
                if (_parsedTarget == null) _parsedTarget = (BuildingTile) target;
                return _parsedTarget;
            }
        }

        public override void OnInspectorGUI () {
            DrawDefaultInspector();

            if (GUI.changed) {
                EditorUtility.SetDirty(Target);
                EditorSceneManager.MarkSceneDirty(Target.gameObject.scene);
            }
        }

        void OnSceneGUI () {

        }
    }
}
