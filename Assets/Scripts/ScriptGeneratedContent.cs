using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class ScriptGeneratedContent {
    public Transform persistentRoot;
    public Transform DisposableRoot { get => _disposableRoot; }
    Transform _disposableRoot;

    void ClearRemainings () {
        Transform remaining;
        for (int guard = 0; guard<100; guard++) {
            remaining = persistentRoot.Find("dispose on clear");
            if (remaining == null) {
                return;
            }

            Util.SafeDestroy(remaining.gameObject);
        }
    }

    public void Clear () {
        if (_disposableRoot != null) {
            Util.SafeDestroy(_disposableRoot.gameObject);
        }

        ClearRemainings();

        _disposableRoot = new GameObject("dispose on clear").transform;
        _disposableRoot.SetParent(persistentRoot, false);
    }

}
