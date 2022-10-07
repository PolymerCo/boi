using System;
using System.Threading.Tasks;
using Source.Math;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Source.Terrain {
    public static class BMapFactory {
        /**
         * The size of the map in units.
         */
        public const int MapSize = 2032;

        /**
         * Number of chunks on the X and Y axis.
         */
        public const int AxisChunks = 16;

        /**
         * The size of a chunk in units.
         */
        public const int ChunkSize = MapSize / AxisChunks;

        /**
         * Sea level in units.
         */
        public const float MapSeaLevel = 8f;

        /** Padding between the sea level and non-beach terrain */
        private const float MapSeaLevelBeachPadding = 1f;

        /** Frequency gain for map foliage (in relation to map terrain frequency) */
        private const float MapFoliageFrequencyGain = 4f;

        /** Threshold to remove all foliage */
        private const float MapFoliageRemovalThreshold = 0.4f;
        
        /** Vertex color of the grass. */
        private static readonly Color GrassColor = new(139 / 255f, 212 / 255f, 61 / 255f);

        /** Vertex color of the sand. */
        private static readonly Color SandColor = new(246 / 255f, 215 / 255f, 175 / 255f);

        /** Seed for the map simplex generation */
        private static int _seed;

        /// <summary>
        /// Generates map data based.
        /// </summary>
        /// <returns>A <see cref="BMapData"/> object containing the map data.</returns>
        public static async Task<BMapData> GenerateMapData() {
            _seed = BRandom.Instance.seed;
            
            var mapData = new BMapData {
                size = MapSize,
                colors = new Color[MapSize, MapSize],
                heights = new float[MapSize, MapSize],
                foliage = new BMapFoliage[MapSize, MapSize],
                seaLevel = MapSeaLevel,
                seed = _seed
            };

            await Task.Run(() => {
                for (int x = 0; x < MapSize; x++) {
                    for (int y = 0; y < MapSize; y++) {
                        float random = BSimplex.Brownian01(_seed, x, y, 
                            BGenerationOptions.Instance.tFrequency,
                            BGenerationOptions.Instance.tIterations, 
                            BGenerationOptions.Instance.tAmplitudePersistence, 
                            BGenerationOptions.Instance.tFrequencyGain);

                        float height = random * BGenerationOptions.Instance.tAmplitude;

                        mapData.heights[x, y] = height;
                        mapData.colors[x, y] = GetHeightColor(height);
                        mapData.foliage[x, y] = GetFoliageColor(x, y, height);
                    }
                }
            });

            return mapData;
        }
        
        /// <summary>
        /// Gets a color for a given height.
        /// </summary>
        /// <param name="height">The height.</param>
        /// <returns></returns>
        private static Color GetHeightColor(float height) {
            if (height < MapSeaLevel + MapSeaLevelBeachPadding) {
                return SandColor;
            }

            return GrassColor;
        }

        /// <summary>
        /// Gets a Foliage type for a given height and position.
        /// </summary>
        /// <param name="x">X position.</param>
        /// <param name="y">Y position.</param>
        /// <param name="height">The height.</param>
        /// <returns></returns>
        private static BMapFoliage GetFoliageColor(int x, int y, float height) {
            if (height < MapSeaLevel + MapSeaLevelBeachPadding) {
                return BMapFoliage.None;
            }

            float random = BSimplex.Brownian01(_seed, x, y, 
                BGenerationOptions.Instance.tFrequency * MapFoliageFrequencyGain,
                BGenerationOptions.Instance.tIterations, 
                BGenerationOptions.Instance.tAmplitudePersistence, 
                BGenerationOptions.Instance.tFrequencyGain);

            if (random < MapFoliageRemovalThreshold) {
                return BMapFoliage.None;
            }

            return BMapFoliage.Grass;
        }
    }
}
