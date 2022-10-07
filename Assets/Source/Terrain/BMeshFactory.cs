using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Source.Terrain {
    public static class BMeshFactory {

        /// <summary>
        /// Generates a mesh for a given BMapData object.
        /// </summary>
        /// <param name="mapData">The BMapData object.</param>
        /// <returns>The mesh.</returns>
        public static async Task<Mesh> FromMapData(BMapData mapData) {
            if (mapData.heights.Length * 4 >= ushort.MaxValue) {
                throw new Exception($"Attempted to generate map mesh who's size will exceed mesh limit: {mapData.heights.Length}");
            }

            int size = mapData.size;
            int totalSize = (int) Mathf.Pow(size, 2);
            
            // how many vertices represent the axis (ie: size + 1 vertices in a straight line in the y axis) 
            int vertexSize = size + 1;
            int totalVertexSize = (int) Mathf.Pow(vertexSize, 2);
            
            var mesh = new Mesh();

            var vertices = new Vector3[totalVertexSize];
            var colors = new Color[totalVertexSize];
            var triangles = new int[totalSize * 6];

            int iVertex = 0;
            int iTris = 0;

            await Task.Run(() => {
                for (int x = 0; x < size; x++) {
                    for (int y = 0; y < size; y++) {
                        float height = mapData.heights[x, y];
                        var color = mapData.colors[x, y];

                        vertices[iVertex] = new Vector3(x, height, y);

                        colors[iVertex] = color;

                        // if on the far x or y axis ignore as triangles do not expand past this
                        if (x < size - 1 && y < size - 1) {
                            triangles[iTris] = iVertex; // bottom left vertex
                            triangles[iTris + 1] = iVertex + 1; // bottom right vertex
                            triangles[iTris + 2] = iVertex + size; // top left vertex
                            triangles[iTris + 3] = iVertex + 1; // bottom right vertex
                            triangles[iTris + 4] = iVertex + size + 1; // top right vertex
                            triangles[iTris + 5] = iVertex + size; // top left vertex
                        }

                        iTris += 6;
                        iVertex += 1;
                    }
                }
            });
            
            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.colors = colors;

            mesh.RecalculateNormals();
            mesh.RecalculateBounds();
            mesh.RecalculateTangents();

            return mesh;
        }
    }
}
