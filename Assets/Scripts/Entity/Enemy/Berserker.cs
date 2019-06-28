using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Berserker : MonoBehaviour {

    [SerializeField] private float pollRate = 5f;
    [SerializeField] private bool canPoll = true;

    private NavMeshAgent agent;

    void Awake() {
        agent = GetComponent<NavMeshAgent>();
    }

    void Start() {
        StartCoroutine(PollAction());
    }

    IEnumerator PollAction() {
        while (canPoll) {
            // Update agent destination
            agent.SetDestination(GameManager.LocalPlayer.transform.position);
            yield return new WaitForSeconds(1 / pollRate);
        }
    }
}
