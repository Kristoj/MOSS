#pragma warning disable 0649
using UnityEngine;

public class PlayerInteraction : MonoBehaviour {

    [SerializeField] private float focusRange = 2.8f;
    [SerializeField] private LayerMask focusMask;
    private Interactable targetInteractable;

    // Update is called once per frame
    void Update() {
        CastFocusRay();
        CheckInput();
    }

    // Check player player input
    void CheckInput() {
        if (Input.GetKeyDown(KeyCode.E) && targetInteractable != null) {
            targetInteractable.OnInteractionStart();
        }
    }

    // Casts a ray to detect if we hit a interactable entity
    // This Interactable object will then be stored inside targetInteractable variable
    void CastFocusRay() {
        targetInteractable = null;
        Ray ray = new Ray(GameManager.LocalPlayer.Player_Camera.MainCamera.transform.position, 
                            GameManager.LocalPlayer.Player_Camera.MainCamera.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, focusRange, focusMask, QueryTriggerInteraction.Collide)) {
            Interactable intera = hit.collider.GetComponent<Interactable>();
            if (intera != null) {
                targetInteractable = intera;
            }
        }
    }
}
