using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Building {
    [RequireComponent(typeof(ScriptGeneratedContent))]
    public class MatrixBuilding : MonoBehaviour {
        #region PREFAB_CONFIG
        #pragma warning disable 0649
        [SerializeField] GameObject prototype;
        [HideInInspector] [SerializeField] ScriptGeneratedContent content;
        #pragma warning restore 0649
        #endregion PREFAB_CONFIG

        public Dictionary<Coord, BuildingCell> pieces =
            new Dictionary<Coord, BuildingCell>();

        public void Generate () {
            content.persistentRoot = transform;
            pieces = new Dictionary<Coord, BuildingCell>();
            content.Clear();

            Add(new Coord(0,0));
        }

        public void Add (Coord pos) {
            BuildingCell created = Instantiate(prototype).AddComponent<BuildingCell>();
            pieces[pos] = created;
            created.tile = created.GetComponent<BuildingTile>();
            created.transform.parent = content.DisposableRoot;
            created.transform.localPosition = pos.ToWorld(FloorTile.tileSize);
        }

        public void Remove (Coord pos) {
            if (pieces.ContainsKey(pos) == false) return;

            Util.SafeDestroy(pieces[pos].gameObject);
            pieces.Remove(pos);
        }
    }
}
