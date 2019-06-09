using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.Collections.Generic;

public class GenericEditor<T> : Editor where T: MonoBehaviour {
    public T Target { get => (T) target; }

    public virtual void CustomInspectorGUI () {}
    public virtual void CustomGizmos () {}

    void SafeSave () {
        if (GUI.changed && !Application.isPlaying) {
            EditorUtility.SetDirty(Target);
            EditorSceneManager.MarkSceneDirty(Target.gameObject.scene);
        }
    }

    // https://docs.unity3d.com/ScriptReference/EditorGUILayout.html
    // https://docs.unity3d.com/ScriptReference/GUILayout.html
    public override void OnInspectorGUI () {
        DrawDefaultInspector();
        CustomInspectorGUI();

        SafeSave();
    }

    public void UselessSceneGUI () {
        CustomGizmos();

        SafeSave();
    }
}
