using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Source.Terrain {
    [RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]
    public class BMapController : MonoBehaviour, IMapController {

        /** Object that represents the ocean */
        public GameObject ocean;

        /** Locally attached mesh renderer */
        private MeshRenderer _meshRenderer;

        /** Locally attached mesh filter */
        private MeshFilter _meshFilter;

        private void Awake() {
            _meshRenderer = GetComponent<MeshRenderer>();
            _meshFilter = GetComponent<MeshFilter>();
        }

        /// <summary>
        /// Generate the map.
        /// </summary>
        public async void GenerateMap() {
            Debug.Log("Generating map..");

            Debug.Log("Generating map data and mesh..");
            
            var mapData = await BMapFactory.GenerateMapData();
            Debug.Log("Map data generated.");
            var mesh = await BMeshFactory.FromMapData(mapData);
            
            Debug.Log("Map data and mesh generation complete.");

            _meshFilter.sharedMesh = mesh;
            
            var position = ocean.transform.position;
            position.Set(position.x, mapData.seaLevel, position.z);

            Debug.Log("Generation complete.");
        }
    }
}
