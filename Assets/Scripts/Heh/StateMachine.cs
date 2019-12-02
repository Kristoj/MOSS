using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine<T> {

    public delegate void StateFinishHandler();
    public event StateFinishHandler OnStateFinish;
    private State<T> currentState;
    private T owner;

    private const int stateHistoryLength = 5;
    private List<State<T>> _stateQueue;
    private List<State<T>> StateQueue {
        get {
            if (_stateQueue == null)
                _stateQueue = new List<State<T>>();
            return _stateQueue;
        }
        set {
            _stateQueue = value;
        }
    }

    public void ClearQueue() {
        StateQueue.Clear();
    }

    private State<T>[] _stateHistory;
    public State<T>[] StateHistory {
        get {
            if (_stateHistory == null)
                _stateHistory = new State<T>[stateHistoryLength];
            return _stateHistory;
        }
    }

    public StateMachine(T newOwner) {
        owner = newOwner;
    }

    public void ChangeState(State<T> newState) {
        // Don't do anything if the new state is NULL
        if (newState == null)
            return;

        // If we already are in a state, we need to exit it
        if (currentState != null) {
            currentState.ExitState(owner);
        }
        else {
            currentState = newState;
            Debug.Log("Entering state: " + currentState.StateName);
            currentState.OnStateExit += ExitStateCallback;
            currentState.EnterState(owner);
        }
    }

    public void QueueState(State<T> state) {
        StateQueue.Add(state);
    }

    public bool HasCompletedStateInXTurns(State<T> state, int count) {
        for (int i = 0; i < count; i++) {
            if (StateHistory[i] != null && StateHistory[i].GetType() == state.GetType()) {
                return true;
            }
        }
        return false;
    }

    public void ExitStateCallback(T owner) {
        // If we have states queued, enter the next state on that list
        if (StateQueue.Count > 0) {
            State<T> state = StateQueue[0];
            StateQueue.RemoveAt(0);
            UpdateStateHistory();
            currentState = null;
            ChangeState(state);
        }
        // If we don't have states queued trigger the OnStateFinish event that the owner can react to
        else if (OnStateFinish != null) {
            UpdateStateHistory();
            currentState = null;
            OnStateFinish?.Invoke();
        }
    }

    void UpdateStateHistory() {
        for (int i = 1; i < stateHistoryLength - 1; i++) {
            if (StateHistory[i] != null)
                StateHistory[i] = StateHistory[i + 1];
        }
        if (currentState != null)
            StateHistory[0] = currentState;
    }

}