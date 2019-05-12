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


        public List<BuildingCell> pieces;

        public void Generate () {
            content.persistentRoot = transform;
            pieces = new List<BuildingCell>();
            content.Clear();

            if (pieces.Count == 0) {
                pieces.Add(AddPiece(Vector3.zero));
            }
        }

        public BuildingCell AddPiece (Vector3 localPosition) {
            BuildingCell cell = Instantiate(prototype).AddComponent<BuildingCell>();
            cell.transform.parent = content.DisposableRoot;
            cell.transform.localPosition = localPosition;
            cell.Initialize();
            return cell;
        }
    }
}
