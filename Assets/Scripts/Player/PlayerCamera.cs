#pragma warning disable 0649
using UnityEngine;

public class PlayerCamera : MonoBehaviour {
    
    [SerializeField] private float mouseSensitivity = 110f;
    private Vector3 camRotation;
    private Transform _head;
    public Transform Head {
        get {
            if (_head == null) {
                FindDependencies();
            }
            return _head;
        }
        set {
            _head = value;
        }
    }
    private Camera _mainCamera;
    public Camera MainCamera {
        get {
            if (_mainCamera ==null) {
                FindDependencies();
            }
            return _mainCamera;
        }
        set {
            _mainCamera = value;
        }
    }

    // Options
    [SerializeField] private bool hideCursor = true;

    void Awake() {
        FindDependencies();
        SetCursorEnabled(!hideCursor);
    }

    void Update() {
        CheckInput();
        CameraLook();
    }

    void CameraLook() {
        // Get mouse input and add it to our cam rotation variable
        Vector2 input = new Vector2(Input.GetAxisRaw("Mouse X"), 
                                    Input.GetAxisRaw("Mouse Y"));
        camRotation.x -= input.y * mouseSensitivity * Time.deltaTime;
        camRotation.y += input.x * mouseSensitivity * Time.deltaTime;  
        camRotation.x = Mathf.Clamp(camRotation.x, -90, 90);            // Prevent camera from turning more than 90 degrees in x-axis

        // Apply rotation
        Head.rotation = Quaternion.Euler(new Vector3(camRotation.x, Head.eulerAngles.y, Head.eulerAngles.z));
        transform.rotation = Quaternion.Euler(new Vector3(transform.eulerAngles.x, camRotation.y, transform.eulerAngles.z));
    }

    void CheckInput() {
        if (Input.GetButtonDown("Fire1")) {
            SetCursorEnabled(false);
        }
        if (Input.GetKeyDown(KeyCode.Escape)) {
            SetCursorEnabled(true);
        }
    }

    void SetCursorEnabled(bool enabled) {
        if (enabled) {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        } else {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    void FindDependencies() {
        // Find head nad camera references
        for (int i = 0; i < transform.childCount; i++) {
            if (transform.GetChild(i).name == "Head") {
                Head = transform.GetChild(i);
                for (int j = 0; j < Head.childCount; j++) {
                    if (Head.GetChild(j).GetComponent<Camera>() != null) {
                        MainCamera = Head.GetChild(j).GetComponent<Camera>();
                    }
                }
            }
        }
    }

}