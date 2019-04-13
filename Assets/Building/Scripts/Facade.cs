using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Building {
    public class Facade : MonoBehaviour {
        public static float tileSize = 4;
        public Dictionary<int, BuildingTileType> tilesInfo =
            new Dictionary<int, BuildingTileType>();
        public List<BuildingTile> tileInstances;
        public GameObject tilePrototype;
        public ScriptGeneratedContent content;

        public Vector3 start;
        public Vector3 end;

        public void Generate () {
            content.Clear();
            tileInstances = new List<BuildingTile>();

            Vector3 forward = start - end;
            int amount = (int) Mathf.Ceil(forward.magnitude / (float) tileSize);
            float scale = forward.magnitude / (amount * tileSize);
            forward.Normalize();
            // tiles are looking to the right
            Quaternion tileRot = Quaternion.LookRotation(forward) *
                Quaternion.Euler(0, 90, 0);

            for (int i=0; i<amount; i++) {
                BuildingTile created = Instantiate(tilePrototype).
                    GetComponent<BuildingTile>();

                if (!tilesInfo.ContainsKey(i)) {
                    tilesInfo[i] = new BuildingTileType();
                    tilesInfo[i].typeOfWall["north"] = WallType.simple;
                }

                created.CurrentType = tilesInfo[i];
                created.transform.parent = content.DisposableRoot;
                created.transform.localRotation = tileRot;
                created.transform.localPosition =
                    end - created.transform.right * i * scale * tileSize;
                created.transform.localScale = new Vector3(scale, 1, 1);
                created.name = i + "";
                tileInstances.Add(created);
            }
        }
    }
}
