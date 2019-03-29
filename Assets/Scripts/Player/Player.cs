using UnityEngine;

public class Player : MonoBehaviour {
    
    #region Player Components
    // Player Controller
    private PlayerController _playerController;
    public PlayerController Player_Controller {
        get {
            if (_playerController == null)
                Debug.LogError("Critical error! Player Controller reference was NULL!");
            return _playerController;
        }
        set {
            _playerController = value;
        }
    }

    // Player Camera
    private PlayerCamera _playerCamera;
    public PlayerCamera Player_Camera {
        get {
            if (_playerCamera == null)
                Debug.LogError("Critical error! Player Camera reference was NULL!");
            return _playerCamera;
        }
        set {
            _playerCamera = value;
        }
    }

    // Weapon System
    private WeaponSystem _weaponSystem;
    public WeaponSystem Player_WeaponSystem {
        get {
            if (_weaponSystem == null) {
                Debug.LogError("Critical error! Player Weapon System was NULL!");
            }
            return _weaponSystem;
        }
        set {
            _weaponSystem = value;
        }
    }

    // Viewmodel
    private Transform _playerViewModel;
    public Transform Player_ViewModel {
        get {
            return _playerViewModel;
        }
        set {
            _playerViewModel = value;
        }
    }

    // Viewmodel Animator
    private ViewModelAnimator _playerViewModelAnimator;
    public ViewModelAnimator Player_ViewModelAnimator {
        get {
            if (_playerViewModelAnimator == null) {
                Debug.LogError("Critical error! View Model Animator object reference was NULL");
            }
            return _playerViewModelAnimator;
        }
        set {
            _playerViewModelAnimator = value;
        }
    }
    #endregion

    void Awake() {
        SetupReferences();
        RegisterPlayer();
    }

    void SetupReferences() {
        Player_Controller = GetComponent<PlayerController>();           // Player Controller
        Player_Camera = GetComponent<PlayerCamera>();                   // Player Camera
        Player_WeaponSystem = GetComponent<WeaponSystem>();             // Weapon System
        Player_ViewModelAnimator = GetComponent<ViewModelAnimator>();   // Viewmodel Animator

        // Player viewmodel
        Transform head = GameManager.LocalPlayer.Player_Camera.Head;
        for (int i = 0; i < head.childCount; i++) {
            if (head.GetChild(i).name == "View_Model_Lowpoly") {
                Player_ViewModel = head.GetChild(i);
                break;
            }
        }
    }

    void RegisterPlayer() {
        GameManager.LocalPlayer = this;
    }
}
