#pragma warning disable 0649
using UnityEngine;

public class WeaponSway : MonoBehaviour {

    [Header("Viewmodel Settings")]
    [SerializeField] private Vector3 viewModelOffset;

    [Header("Mouse Sway")]
    [SerializeField] private Vector2 mouseSwayRange = new Vector2(.07f, .07f);
    [SerializeField] private Vector2 mouseSwaySpeed = new Vector2(.05f, .05f);
    [SerializeField] private Vector2 mouseSwayReturnSpeed = new Vector2(10f, 10f);

    [Header("Movement Sway")]
    [SerializeField] private Vector3 movementSwayRange = new Vector3(.05f, .05f, .05f);
    [SerializeField] private Vector3 movementSwaySpeed = new Vector3(.05f, .05f, .05f);
    [SerializeField] private Vector3 movementSwayReturnSpeed = new Vector3(7f, 7f, 7f);

    [Header("Mouse Tilt")]
    [SerializeField] private Vector3 mouseTiltRange = new Vector3(3f, 3f, 3f);
    [SerializeField] private Vector3 mouseTiltSpeed = new Vector3(1f, 1f, 1f);
    [SerializeField] private Vector3 mouseTiltReturnSpeed = new Vector3(5f, 5f, 5f);

    [Header("Movement Tilt")]
    [SerializeField] private Vector3 movementTiltRange = new Vector3(3f, 3f, 3f);
    [SerializeField] private Vector3 movementTiltSpeed = new Vector3(1f, 1f, 1f);
    [SerializeField] private Vector3 movementTiltReturnSpeed = new Vector3(5f, 5f, 5f);


    // Sway & Tilt vectors
    private Vector3 mouseSwayVector;
    private Vector3 movementSwayVector;
    public Vector3 mouseTiltVector;
    public Vector3 movementTiltVector;

    void Update() {
        // Sway
        MouseSwayAccelerate();
        MouseSwayReturn();
        MovementSwayAccelerate();
        MovementSwayReturn();
        // Tilt
        MouseTiltAccelerate();
        MouseTiltReturn();
        MovementTiltAccelerate();
        MovementTiltReturn();
        ApplySocketOrientation();
    }

    void MouseSwayAccelerate() {
        Vector2 input = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        mouseSwayVector.x += input.x * mouseSwaySpeed.x * Time.deltaTime;
        mouseSwayVector.y += input.y * mouseSwaySpeed.y * Time.deltaTime;

        // Clamp the sway vector
        mouseSwayVector.x = Mathf.Clamp(mouseSwayVector.x, -mouseSwayRange.x, mouseSwayRange.x);
        mouseSwayVector.y = Mathf.Clamp(mouseSwayVector.y, -mouseSwayRange.y, mouseSwayRange.y);
    }

    void MouseSwayReturn() {
        mouseSwayVector.x = Mathf.Lerp(mouseSwayVector.x, 0, mouseSwayReturnSpeed.x* Time.deltaTime);
        mouseSwayVector.y = Mathf.Lerp(mouseSwayVector.y, 0, mouseSwayReturnSpeed.y * Time.deltaTime);
    }

    void MouseTiltAccelerate() {
        Vector2 input = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxis("Mouse Y"));
        mouseTiltVector.x += input.y * mouseTiltSpeed.x * Time.deltaTime;
        mouseTiltVector.y += input.x * mouseTiltSpeed.y * Time.deltaTime;
        mouseTiltVector.z += input.x * mouseTiltSpeed.z * Time.deltaTime;

        // Clamp mouse tilt vector
        mouseTiltVector.x = Mathf.Clamp(mouseTiltVector.x, -mouseTiltRange.x, mouseTiltRange.x);
        mouseTiltVector.y = Mathf.Clamp(mouseTiltVector.y, -mouseTiltRange.y, mouseTiltRange.y);
        mouseTiltVector.z = Mathf.Clamp(mouseTiltVector.z, -mouseTiltRange.z, mouseTiltRange.z);
    }

    void MouseTiltReturn() {
        mouseTiltVector.x = Mathf.Lerp(mouseTiltVector.x, 0, mouseTiltReturnSpeed.x * Time.deltaTime);
        mouseTiltVector.y = Mathf.Lerp(mouseTiltVector.y, 0, mouseTiltReturnSpeed.y * Time.deltaTime);
        mouseTiltVector.z = Mathf.Lerp(mouseTiltVector.z, 0, mouseTiltReturnSpeed.z * Time.deltaTime);
    }

    void MovementSwayAccelerate() {
        // Add movement sway according to player input
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        movementSwayVector.x -= input.x * movementSwaySpeed.x * Time.deltaTime;
        movementSwayVector.z -= input.y * movementSwaySpeed.z * Time.deltaTime;
        
        // Clamp movement sway vector
        movementSwayVector.x = Mathf.Clamp(movementSwayVector.x, -movementSwayRange.x, movementSwayRange.z);
        movementSwayVector.y = Mathf.Clamp(movementSwayVector.y, -movementSwayRange.y, movementSwayRange.y);
        movementSwayVector.z = Mathf.Clamp(movementSwayVector.z, -movementSwayRange.z, movementSwayRange.z);
    }

    void MovementSwayReturn() {
        movementSwayVector.x = Mathf.Lerp(movementSwayVector.x, 0, movementSwayReturnSpeed.x * Time.deltaTime);
        movementSwayVector.y = Mathf.Lerp(movementSwayVector.y, 0, movementSwayReturnSpeed.y * Time.deltaTime);
        movementSwayVector.z = Mathf.Lerp(movementSwayVector.z, 0, movementSwayReturnSpeed.z * Time.deltaTime);
    }

    void MovementTiltAccelerate() {
        // Get user player movement input and add it to the movement tilt vector
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        movementTiltVector.x += input.y * movementTiltSpeed.x * Time.deltaTime;
        movementTiltVector.y += input.x * movementTiltSpeed.y * Time.deltaTime;
        movementTiltVector.z -= input.x * movementTiltSpeed.z * Time.deltaTime;

        // Clamp movement tilt vector
        movementTiltVector.x = Mathf.Clamp(movementTiltVector.x, -movementTiltRange.x, movementTiltRange.x);
        movementTiltVector.y = Mathf.Clamp(movementTiltVector.y, -movementTiltRange.y, movementTiltRange.y);
        movementTiltVector.z = Mathf.Clamp(movementTiltVector.z, -movementTiltRange.z, movementTiltRange.z);
    }

    void MovementTiltReturn() {
        movementTiltVector.x = Mathf.Lerp(movementTiltVector.x, 0, movementTiltReturnSpeed.x * Time.deltaTime);
        movementTiltVector.y = Mathf.Lerp(movementTiltVector.y, 0, movementTiltReturnSpeed.y * Time.deltaTime);
        movementTiltVector.z = Mathf.Lerp(movementTiltVector.z, 0, movementTiltReturnSpeed.z * Time.deltaTime);
    }

    void ApplySocketOrientation() {
        Transform viewModel = GameManager.LocalPlayer.Player_ViewModel;
        viewModel.transform.localPosition = viewModelOffset + mouseSwayVector + movementSwayVector;
        viewModel.transform.localRotation = Quaternion.Euler(mouseTiltVector + movementTiltVector);
    }

}
