using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

namespace Source.Math {
    /// <summary>
    /// Manages the Unity Random state.
    /// </summary>
    public class BRandom : MonoBehaviour, IRandom {
        /** Singleton instance of BRandom */
        public static BRandom Instance { get; private set; }
        
        /// <summary>
        /// Seed value for random generation.
        /// </summary>
        public int seed = 123456789;

        private void Awake() {
            if (Instance is not null) {
                Debug.LogWarning("Attempted creation of new BRandom when one already exists: destroyed");
                Destroy(this);
            }
            
            Instance = this;
            InitState();
        }

        private void InitState() {
            Random.InitState(seed);
        }

        /// <summary>
        /// Sets the seed and resets the state based on a new seed value.
        /// </summary>
        /// <param name="newSeed">The new seed value.</param>
        public void SetSeed(int newSeed) {
            seed = newSeed;
            Debug.Log($"new seed set: {seed}");
            InitState();
        }

        /// <summary>
        /// Sets the seed and resets the state based on a string seed value.
        /// </summary>
        /// <param name="newSeed">The new string seed value.</param>
        public void SetSeed(string newSeed) {
            seed = newSeed.GetHashCode();
            Debug.Log($"new seed set: {seed}");
            InitState();
        }
    }
}
