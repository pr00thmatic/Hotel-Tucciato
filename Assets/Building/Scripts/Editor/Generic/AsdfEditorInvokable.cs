using UnityEngine;
using UnityEditor;

public class AsdfEditorInvokable : EditorInvokable<Asdf> {
    public override void DrawGizmos (Asdf target) {
        Handles.DrawSolidDisc(target.transform.position, Vector3.up, 2f);
    }

    public override void DrawInspectorGUI (Asdf target) {}
}
