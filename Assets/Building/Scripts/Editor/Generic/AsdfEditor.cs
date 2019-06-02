using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Asdf))]
public class AsdfEditor: GenericEditor<Asdf> {
    static EditorInvokable<Asdf>[] _invokables =
        new EditorInvokable<Asdf>[] { new AsdfEditorInvokable() };
    public override EditorInvokable<Asdf>[] Invokables { get => _invokables; }

    public override void CustomInspectorGUI () {
        GUILayout.Toggle(true, "XD", "Button");
    }

    void OnSceneGUI () {
        UselessSceneGUI();
    }
}
