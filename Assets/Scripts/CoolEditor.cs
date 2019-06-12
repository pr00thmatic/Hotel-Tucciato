using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class CoolEditor {
    public static Tool old = Tool.None;

    public static void HideTool () {
        if (Tools.current == Tool.None) return;
        old = Tools.current;
        Tools.current = Tool.None;
    }

    public static void ShowTool () {
        if (old == Tool.None) {
            old = Tool.Move;
        }
        Tools.current = old;
    }
}
