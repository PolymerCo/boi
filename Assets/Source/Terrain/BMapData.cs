using System;
using UnityEngine;

namespace Source.Terrain {
    public class BMapData {
        public int size;
        public float[,] heights;
        public Color[,] colors;
        public BMapFoliage[,] foliage;
        public float seaLevel;
        public int seed;

        /// <summary>
        /// Get <see cref="BMapChunkData"/> that would represent a given chunk.
        /// </summary>
        /// <param name="indexX">X index of the chunk. (ie: the nth chunk in the x direction)</param>
        /// <param name="indexY">Y index of the chunk. (ie: the nth chunk in the y direction)</param>
        /// <returns>The <see cref="BMapChunkData"/>.</returns>
        public BMapChunkData GetChunkData(int indexX, int indexY) {
            if (indexX is < 0 or >= BMapFactory.AxisChunks) {
                throw new ArgumentException($"Attempted to get chunk on invalid X index value: {indexX}");
            }
            
            if (indexY is < 0 or >= BMapFactory.AxisChunks) {
                throw new ArgumentException($"Attempted to get chunk on invalid Y index value: {indexY}");
            }

            const int length = BMapFactory.ChunkSize;
            int offsetX = indexX * length;
            int offsetY = indexY * length;
            
            float

            return;
        }
    }
}