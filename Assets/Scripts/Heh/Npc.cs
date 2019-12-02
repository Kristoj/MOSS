using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour {

    [SerializeField] private Vector2 idleTime = new Vector2(3, 9);
    private StateMachine<Npc> stateMachine;

    void Awake() {
        // Setup the state machine
        stateMachine = new StateMachine<Npc>(this);
        stateMachine.OnStateFinish += EvaluateStates;
        stateMachine.ChangeState(new State_Idle());
    }

    // This is called when the state machine exits it current state
    // and will determine the next state
    void EvaluateStates() {
        // Array that contains every possible state
        State<Npc>[] states = new State<Npc>[] {
            new State_Idle(),
            new State_Move()
        };
        // Get the highest score from all possible states
        float highestScore = float.MinValue;
        State<Npc> chosenState = null;
        for (int i = 0; i < states.Length; i++) {
            float score = states[i].GetScore(this);
            if (score > highestScore) {
                highestScore = score;
                chosenState = states[i];
            }
        }
        // Enter the chosen state
        if (chosenState != null) {
            Debug.Log(chosenState.StateName + " got the highest score of " + highestScore + ".");
            stateMachine.ChangeState(chosenState);
        }
    }

    class State_Idle : State<Npc> {
        public override string StateName => "Idle";
        private Coroutine idleCoroutine;

        public override void EnterState(Npc owner) {
            idleCoroutine = owner.StartCoroutine(Idle(owner));
        }

        IEnumerator Idle(Npc owner) {
            yield return new WaitForSeconds(Random.Range(owner.idleTime.x, owner.idleTime.y));
            ExitState(owner);
        }

        public override void ExitState(Npc owner) {
            if (idleCoroutine != null)
                owner.StopCoroutine(idleCoroutine);
            base.ExitState(owner);
        }

        public override float GetScore(Npc owner) {
            return 5;
        }
    }

    class State_Move : State<Npc> {
        public override string StateName => "Move";
        private Coroutine distanceCoroutine;

        public override void EnterState(Npc owner) {
            owner.StartCoroutine(DistanceCheck(owner));
        }

        IEnumerator DistanceCheck(Npc owner) {
            yield return new WaitForSeconds(1);
            ExitState(owner);
        }

        public override void ExitState(Npc owner) {
            owner.stateMachine.QueueState(new State_Rest());
            owner.stateMachine.QueueState(new State_Drink());
            base.ExitState(owner);
        }

        public override float GetScore(Npc owner) {
            float score = 21;
            if (owner.stateMachine.HasCompletedStateInXTurns(this, 1)) {
                score -= 20;
            }
            return score;
        }
    }

    class State_Rest : State<Npc> {
        public override string StateName => "Rest";

        public override void EnterState(Npc owner) {
            owner.StartCoroutine(Rest(owner));
        }

        IEnumerator Rest(Npc owner) {
            yield return new WaitForSeconds(2);
            ExitState(owner);
        }

        public override float GetScore(Npc owner) {
            return 0;
        }
    }

    class State_Drink : State<Npc> {
        public override string StateName => "Drink";

        public override void EnterState(Npc owner) {
            owner.StartCoroutine(Drink(owner));
        }

        IEnumerator Drink(Npc owner) {
            yield return new WaitForSeconds(2);
            ExitState(owner);
        }

        public override float GetScore(Npc owner) {
            return 0;
        }
    }
}
