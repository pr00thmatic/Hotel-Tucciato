using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.Collections.Generic;

public class GenericEditor<T> : Editor where T: MonoBehaviour {
    public T Target { get => (T) target; }

    public virtual EditorInvokable<T>[] Invokables { get => new EditorInvokable<T>[0]; }

    public virtual void CustomInspectorGUI () {}
    public virtual void CustomGizmos () {}

    void OnEnable () {
        foreach (var invokable in Invokables) {
            invokable.Initialize(Target);
        }
    }

    void OnDisable () {
        foreach (var invokable in Invokables) {
            invokable.Destroy(Target);
        }
    }

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
        foreach (var invokable in Invokables) {
            invokable.DrawInspectorGUI(Target);
        }
        SafeSave();
    }

    public void UselessSceneGUI () {
        CustomGizmos();
        foreach (var invokable in Invokables) {
            invokable.DrawGizmos(Target);
        }
        SafeSave();
    }
}
