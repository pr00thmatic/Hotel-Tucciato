using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class CoolEditor {
    public static Color oldColor = Color.white;
    public static Tool oldTool = Tool.None;
    public static Matrix4x4 oldMatrix = Matrix4x4.identity;

    public static void HideTool () {
        if (Tools.current == Tool.None) return;
        oldTool = Tools.current;
        Tools.current = Tool.None;
    }

    public static void ShowTool () {
        if (oldTool == Tool.None) {
            oldTool = Tool.Move;
        }
        Tools.current = oldTool;
    }

    public static void SetHandlesMatrix (Matrix4x4 matrix) {
        oldMatrix = Handles.matrix;
        Handles.matrix = matrix;
    }

    public static void RestoreHandlesMatrix () {
        Handles.matrix = oldMatrix;
    }

    public static void SetHandlesColor (Color color) {
        oldColor = Handles.color;
        Handles.color = color;
    }

    public static void RestoreHandlesColor () {
        Handles.color = oldColor;
    }
}
