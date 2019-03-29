using UnityEngine;

public class WeaponSystem : MonoBehaviour {

    [SerializeField] private Weapon[] weaponSlots = new Weapon[2];
    [SerializeField] private Weapon equippedWeapon;

    // Right socket
    [SerializeField] private Transform _socketR;
    public Transform SocketR {
        get {
            if (_socketR == null) {
                FindDependecies();
            }
            return _socketR;
        }
        set {
            _socketR = value;
        }
    }

    // Left socket
    [SerializeField] private Transform _socketL;
    public Transform SocketL {
        get {
            if (_socketL == null) {
                FindDependecies();
            }
            return _socketL;
        }
        set {
            _socketL = value;
        }
    }

    void Start() {
        FindDependecies();
        // Spawn starting weapon ?
        for (int i = 0; i < weaponSlots.Length; i++) {
            if (weaponSlots[i] != null) {
                EquipWeapon(weaponSlots[i]);
                break;
            }
        }
    }

    void Update() {
        CheckInput();
    }

    // Handles weapon equipping. Checks if there's a weapon already in hand etc...
    void EquipWeapon(Weapon weaponToEquip) {

        // If weapon we want to equip is null print a error message and return
        if (weaponToEquip == null) {
            Debug.LogError("Error! Weapon we wanted to equip was NULL.");
            return;
        }

        // If we already have a weapon equipped, destroy it
        if (equippedWeapon != null) {
            DestroyWeapon(equippedWeapon);
        }
        SpawnWeapon(weaponToEquip);     
    }

    // Spawns the weapon in the socket player equips a weapon
    void SpawnWeapon(Weapon weaponToSpawn) {
        equippedWeapon = Instantiate(weaponToSpawn, SocketR.transform.position, SocketR.transform.rotation);
        equippedWeapon.transform.SetParent(SocketR);
    }

    // Destroys the given weapon, but doesn't unassign weapons from weapon slots
    void DestroyWeapon(Weapon weaponToDestroy) {
        if (weaponToDestroy != null) {
            Destroy(weaponToDestroy.gameObject);
        }
    }
    
    // Checks player input if X key was pressed
    void CheckInput() {

        // Shooting
        if (Input.GetButton("Fire1") && equippedWeapon != null) {
            equippedWeapon.Shoot();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            EquipWeapon(weaponSlots[0]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            EquipWeapon(weaponSlots[1]);
        }
    }

    void FindDependecies() {
        /*
        Transform head = GameManager.LocalPlayer.Player_Camera.Head;
        Transform target = FindChild(head, "View_Model_Lowpoly");
        if (target != null) {
            // SocketR
            target = FindChild(target, "Armature");
            if (target != null) {
                //target = FindChild(target, );
            }
            // SocketL
        }
        */
    }

    // TODO : This should be moved to somewhere else!
    Transform FindChild(Transform root, string childName) {
        for (int i = 0; i < root.childCount; i++) {
            if (root.GetChild(i).name == childName) {
                return root.GetChild(i);
            }
        }
        return null;
    }

}
