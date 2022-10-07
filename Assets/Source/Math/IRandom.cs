using UnityEngine.EventSystems;

namespace Source.Math {
    public interface IRandom : IEventSystemHandler {
        /// <summary>
        /// Sets the seed and resets the state based on a new seed value.
        /// </summary>
        /// <param name="newSeed">The new seed value.</param>
        public void SetSeed(int newSeed);

        /// <summary>
        /// Sets the seed and resets the state based on a string seed value.
        /// </summary>
        /// <param name="newSeed">The new string seed value.</param>
        public void SetSeed(string newSeed);
    }
}
