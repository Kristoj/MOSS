using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State<T> {

    public delegate void ExitStateHandler(T owner);
    public event ExitStateHandler OnStateExit;
    public abstract string StateName { get; }
    public abstract void EnterState(T owner);
    public virtual void ExitState(T owner) {
        OnStateExit?.Invoke(owner);
        OnStateExit = null;
    }
    public virtual void OnTick(T owner) { }
    public abstract float GetScore(T owner);

}