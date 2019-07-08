using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Building {
    [RequireComponent(typeof(ScriptGeneratedContent))]
    public class MatrixBuilding : MonoBehaviour {
        #region PREFAB_CONFIG
        #pragma warning disable 0649
        [SerializeField] GameObject prototype;
        [SerializeField] ScriptGeneratedContent content;
        #pragma warning restore 0649
        #endregion PREFAB_CONFIG

        public Dictionary<Coord, BuildingCell> pieces =
            new Dictionary<Coord, BuildingCell>();
        [HideInInspector]
        public List<string> serializedBuilding;

        public void Generate () {
            content.persistentRoot = transform;
            pieces = new Dictionary<Coord, BuildingCell>();
            content.Clear();

            Add(new Coord(0,0));
        }

        public void Add (Coord pos) {
            BuildingCell created = Instantiate(prototype).AddComponent<BuildingCell>();
            created.name = pos.ToString();
            created.Initialize();
            pieces[pos] = created;
            created.tile = created.GetComponent<BuildingTile>();
            created.transform.parent = content.DisposableRoot;
            created.transform.localRotation = Quaternion.identity;
            created.transform.localPosition = pos.ToWorld();
        }

        public void Remove (Coord pos) {
            if (pieces.ContainsKey(pos) == false) return;

            Util.SafeDestroy(pieces[pos].gameObject);
            pieces.Remove(pos);
        }

        public MatrixBuildingState GetState () {
            MatrixBuildingState state = new MatrixBuildingState();
            foreach (Transform cellGO in content.DisposableRoot) {
                BuildingCell cell = cellGO.GetComponent<BuildingCell>();
                state.cells.Add(cell.GetState());
            }

            return state;
        }

        public void PopulatePiecesInfo () {
            pieces = new Dictionary<Coord, BuildingCell>();
            foreach (Transform cellGO in content.DisposableRoot) {
                BuildingCell cell = cellGO.GetComponent<BuildingCell>();
                pieces[Coord.FromWorld(cell.transform.localPosition,
                                       FloorTile.tileSize)] = cell;
            }
        }
    }
}
