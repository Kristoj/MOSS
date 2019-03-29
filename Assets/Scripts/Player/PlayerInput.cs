using UnityEngine;

public static class PlayerInput {

    public static bool leftMouseDown = false;
    public static bool rightMouseDown = false;

    public static void Update() {
        UpdateMouseButtonStates();
    }

    static void UpdateMouseButtonStates() {
        // Left mouse down
        if (Input.GetButtonDown("Fire1")) {
            leftMouseDown = true;
        }
        // Left mouse up
        if (Input.GetButtonUp("Fire1")) {
            leftMouseDown = false;
        }
        // Right mouse down
        if (Input.GetButtonDown("Fire2")) {
            rightMouseDown = true;
        }
        // Right mouse up
        if (Input.GetButtonUp("Fire2")) {
            rightMouseDown = false;
        }
    }

}
