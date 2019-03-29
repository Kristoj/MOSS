using UnityEngine;

public static class GameManager {

    // Local player 
    private static Player _localPlayer;
    public static Player LocalPlayer
    {
        get {
            if (_localPlayer == null) {
                GameObject player = GameObject.Find("Player");
                if (player != null) {
                    _localPlayer = player.GetComponent<Player>();
                }
                if (_localPlayer == null) {
                    Debug.LogError("Couldn't not find player reference!");
                }
            }
            return _localPlayer;
        }
        set {
            _localPlayer = value;
        }
    }

}
