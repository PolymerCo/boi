using System;
using UnityEngine;

namespace Source.Math {
    /// <summary>
    /// Manages the simplex generation options.
    /// </summary>
    [Serializable]
    public class BGenerationOptions : MonoBehaviour, IGenerationOptions {
        /** Instance of the singleton <see cref="BGenerationOptions"/> object. */
        public static BGenerationOptions Instance { get; private set; }
        
        /** Terrain generation frequency */
        public float tFrequency = 0.01f;

        /** Terrain generation scale */
        public float tAmplitude = 32f;

        /** Terrain generation iterations */
        public int tIterations = 12;

        /** Terrain generation scale */
        public float tFrequencyGain = 2f;

        /** Terrain generation persistence */
        public float tAmplitudePersistence = 0.5f;

        private void Awake() {
            if (Instance is not null) {
                Destroy(this);
            }

            Instance = this;
        }
    }
}
