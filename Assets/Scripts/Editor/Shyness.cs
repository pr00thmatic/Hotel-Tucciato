using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using Building;
using System.Collections.Generic;

[CustomEditor(typeof(Shyness))]
public class ShynessEditor : GenericEditor<Shyness> {
    void OnSceneGUI () {
        CoolEditor.SetHandlesColor(new Color(1,1,0,1));
        UselessSceneGUI();
        Handles.ArrowHandleCap(0, Target.transform.position + Target.GlobalForward * 0.5f,
                               Quaternion.LookRotation(Target.GlobalForward),
                               1, EventType.Repaint);
        CoolEditor.RestoreHandlesColor();
    }
}
