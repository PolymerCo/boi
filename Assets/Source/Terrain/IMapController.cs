using UnityEngine.EventSystems;

namespace Source.Terrain {
    public interface IMapController : IEventSystemHandler {
        /// <summary>
        /// Generate the map.
        /// </summary>
        public void GenerateMap();
    }
}