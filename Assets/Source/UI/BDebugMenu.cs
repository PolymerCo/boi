using System;
using Source.Math;
using Source.Terrain;
using UnityEngine;
using UnityEngine.EventSystems;

public class BDebugMenu : MonoBehaviour {
    /// <summary>
    /// GameObject that manages the overall system.
    /// </summary>
    public GameObject system;

    /// <summary>
    /// GameObject that manages the map controller.
    /// </summary>
    public GameObject mapController;
    
    private string _userSeed;

    private void Awake() {
        _userSeed = BRandom.Instance.seed.ToString();
    }

    private void OnGUI() {
        CreateWorldOptions();
    }

    private void CreateWorldOptions() {
        GUI.Label(new Rect(10, 10, 100,20), "World Options");
        _userSeed = GUI.TextField(new Rect(10, 35, 100, 20), _userSeed);

        if (GUI.Button(new Rect(10, 60, 100, 20), "Set Seed")) {
            ExecuteEvents.Execute<IRandom>(system, null, (handler, _) => handler.SetSeed(_userSeed));
            _userSeed = BRandom.Instance.seed.ToString();
        }
        
        if (GUI.Button(new Rect(10, 85, 100, 20), "Generate")) {
            ExecuteEvents.Execute<IMapController>(mapController, null, (handler, _) => handler.GenerateMap());
        }
    }
}
