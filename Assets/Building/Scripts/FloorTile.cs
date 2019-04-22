using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

namespace Building {
    [SelectionBase]
    public class FloorTile : MonoBehaviour {
        public static float tileSize = 4;
        public List<BuildingTileState> tileStates;
        public List<BuildingTile> tileInstances;
        public GameObject tilePrototype;
        public ScriptGeneratedContent content;

        public Vector3 start;
        public Vector3 end;

        public void UpdateTilesInfoSize (int newSize) {
            for (int i=0; i<=(newSize-tileStates.Count); i++) {
                AddBlankTileTypeInfo();
            }
        }

        public void ClearTilesInfo () {
            tileStates = new List<BuildingTileState>();
            for (int i=0; i<tileInstances.Count; i++) AddBlankTileTypeInfo();
        }

        public void SaveState () {
            for (int i=0; i<tileInstances.Count; i++) {
                tileStates[i] = tileInstances[i].GetState();
            }
        }

        public void Generate () {
            if (content.persistentRoot == null) {
                content.persistentRoot = transform;
            }

            if (tilePrototype == null) return;

            SaveState();
            content.Clear();
            tileInstances = new List<BuildingTile>();
            if (start == end) return;

            Vector3 forward = start - end;
            int amount =
                (int) Mathf.Min(20, Mathf.Ceil(forward.magnitude / (float) tileSize));
            UpdateTilesInfoSize(amount);
            float scale = forward.magnitude / (amount * tileSize);
            forward.Normalize();
            // tiles are looking to the right
            Quaternion tileRot = Quaternion.LookRotation(forward) *
                Quaternion.Euler(0, 90, 0);

            for (int i=0; i<amount; i++) {
                #if UNITY_EDITOR
                BuildingTile created =
                    ((GameObject) PrefabUtility.InstantiatePrefab(tilePrototype)).
                    GetComponent<BuildingTile>();
                #else
                BuildingTile created = Instantiate(tilePrototype).
                    GetComponent<BuildingTile>();
                #endif

                created.SetState(tileStates[i]);
                created.transform.parent = content.DisposableRoot;
                created.transform.localRotation = tileRot;
                created.transform.localPosition =
                    end - created.transform.right * i * scale * tileSize;
                created.transform.localScale = new Vector3(scale, 1, 1);
                created.name = i + "";
                tileInstances.Add(created);
            }

            if (amount >= 20) {
                Debug.LogWarning("the amount of required tiles is bigger than supported");
            }
        }

        public void SetTileInfo (int index, BuildingTileState type) {
            UpdateTilesInfoSize(index+1);
            tileStates[index] = type;
        }

        void AddBlankTileTypeInfo () {
            tileStates.Add(new BuildingTileState());
        }
    }
}
