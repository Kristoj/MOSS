using UnityEngine;

public class ViewModelAnimator : MonoBehaviour {
    
    private float locomotionSpeed = 0;

    public enum ActionClip {
        Blaster_Shoot = 0
    }

    private Animator animator;

    void Awake() {
        FindDependencies();
    }

    void Update() {
        UpdateLocomotion();
    }

    void UpdateLocomotion() {
        Vector2 velocity = new Vector2(GameManager.LocalPlayer.Player_Controller.velocity.x, GameManager.LocalPlayer.Player_Controller.velocity.z);
        locomotionSpeed = Mathf.MoveTowards(locomotionSpeed, velocity.magnitude / GameManager.LocalPlayer.Player_Controller.moveSpeed, 2.2f * Time.deltaTime);
        animator.SetFloat("velocity", locomotionSpeed);
    }

    public void PlayAction(float actionId) {
        animator.SetFloat("fireId", actionId);
        animator.SetTrigger("Fire");
    }

    void FindDependencies() {
        animator = GetComponent<Animator>();
    }

}
