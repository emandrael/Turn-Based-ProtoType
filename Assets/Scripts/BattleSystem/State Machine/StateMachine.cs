using UnityEngine;
using System.Collections;

public abstract class StateMachine : MonoBehaviour
{
    protected State State;

    public void SetState(State state)
    {
        State = state;
        StartCoroutine(State.Start());
    }


    public void SetAttackState(State state)
    {
        State = state;
        StartCoroutine(State.Attack());
    }


}
