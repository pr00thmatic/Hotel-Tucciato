using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Building {
    // GUILayout.Button("label")
    // Handles.Button(pos, rot, size1, size2, Handles.RectangleHandleCap)
    // Handles.PositionHandle(pos, rot)
    // EditorUtility.SetDirty(gameObject);
    [CustomEditor(typeof(FloorTile))]
    public class FloorTileEditor : GenericEditor<FloorTile> {
        void EndAndStart () {
            Vector3 forward = Target.start - Target.end;
            Quaternion rot = forward == Vector3.zero? Quaternion.identity:
                Quaternion.LookRotation(forward);
            Vector3 start = Handles.PositionHandle(Target.start, rot);
            Vector3 end = Handles.
                PositionHandle(Target.end,  rot * Quaternion.Euler(0, 180, 0));
            bool shouldGenerate = start != Target.start || end != Target.end;

            if (start == end && forward != Vector3.zero) {
                start = Target.start; end = Target.end;
            } else if (forward == Vector3.zero) {
                Target.end = Target.start + new Vector3(1, 0, 0);
            }

            Target.start = start; Target.end = end;


            if (shouldGenerate) {
                Target.Generate();
            }
        }

        bool BuildingTileEdition () {
            bool clicked = false;

            if (Target.tileInstances == null) return false;

            foreach (BuildingTile tile in Target.tileInstances) {
                BuildingTileEditor.DrawGizmos(tile);
            }

            return clicked;
        }

        public override void CustomInspectorGUI () {
            if (GUILayout.Button("Generate")) Target.Generate();
            if (GUILayout.Button("Reset")) {
                Target.ClearTilesInfo();
                Target.Generate();
            }
        }

        void OnSceneGUI () {
            Handles.matrix = Target.transform.localToWorldMatrix;
            Tools.current = Tool.None;

            EndAndStart();
            if (BuildingTileEdition()) {
                Target.Generate();
            }
            UselessSceneGUI();
        }
    }
}
