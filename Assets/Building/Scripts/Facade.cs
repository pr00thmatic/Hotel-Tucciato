using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Building {
    [SelectionBase]
    public class Facade : MonoBehaviour {
        public static float tileSize = 4;
        public List<BuildingTileType> tilesInfo;
        public List<BuildingTile> tileInstances;
        public GameObject tilePrototype;
        public ScriptGeneratedContent content;

        public Vector3 start;
        public Vector3 end;

        public void UpdateTilesInfoSize (int newSize) {
            for (int i=0; i<=(newSize-tilesInfo.Count); i++) {
                AddBlankTileTypeInfo();
            }
        }

        public void ClearTilesInfo () {
            tilesInfo = new List<BuildingTileType>();
            for (int i=0; i<tileInstances.Count; i++) AddBlankTileTypeInfo();
        }

        public void Generate () {
            if (content.persistentRoot == null) {
                content.persistentRoot = transform;
            }

            if (tilePrototype == null) return;

            content.Clear();
            if (start == end) return;
            tileInstances = new List<BuildingTile>();

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
                BuildingTile created = Instantiate(tilePrototype).
                    GetComponent<BuildingTile>();

                created.CurrentType = tilesInfo[i];
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

        public void SetTileInfo (int index, BuildingTileType type) {
            UpdateTilesInfoSize(index+1);
            tilesInfo[index] = type;
        }

        void AddBlankTileTypeInfo () {
            tilesInfo.Add(new BuildingTileType());
        }
    }
}
