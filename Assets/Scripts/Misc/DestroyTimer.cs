using System.Collections;
using UnityEngine;

public class DestroyTimer : MonoBehaviour {

    [SerializeField] private float destroyTime = 10f;

    void Start() {
        StartCoroutine(DestroyDelay());
    }

    IEnumerator DestroyDelay() {
        yield return new WaitForSeconds(destroyTime);
        Destroy(this.gameObject);
    }

}
