using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Asdf))]
public class AsdfEditor: GenericEditor<Asdf> {
    public static void DrawGizmos (Asdf target) {
        Handles.DrawSolidDisc(target.transform.position, Vector3.up, 2f);
    }

    public override void CustomInspectorGUI () {
        GUILayout.Toggle(true, "XD", "Button");
    }

    void OnSceneGUI () {
        DrawGizmos(Target);
        UselessSceneGUI();
    }
}
