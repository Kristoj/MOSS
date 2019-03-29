using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float acceleration = 50f;

    private Rigidbody rig;
    private LivingEntity livingEntity;
    private Animator animator;

    void Awake() {
        FindDependencies();
        SetupCallbacks();
    }

    void Update() {
        LookAtPlayer();
        Accelerate();
    }

    void LookAtPlayer() {
        transform.LookAt(GameManager.LocalPlayer.transform);
    }

    void Accelerate() {
        rig.AddForce(transform.forward * acceleration * Time.deltaTime);
        rig.velocity = Vector3.ClampMagnitude(rig.velocity, moveSpeed);
        animator.SetFloat("moveSpeed", rig.velocity.magnitude);
    }

    void FindDependencies() {
        livingEntity = GetComponent<LivingEntity>();
        animator = GetComponent<Animator>();
        rig = GetComponent<Rigidbody>();
    }

    void SetupCallbacks() {
        livingEntity.HitEvent += HitEventCallback;
        livingEntity.DeathEvent += DeathEventCallback;
    }

    void HitEventCallback() {
        float rng = Random.Range(0f, 2f);
        //Debug.Log(animator.Trig);
        animator.SetFloat("flinchId", rng);
        animator.SetTrigger("FlinchStart");
    }

    void DeathEventCallback() {

    }

}
