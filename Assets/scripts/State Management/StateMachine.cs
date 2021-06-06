using UnityEngine;

public class StateMachine : MonoBehaviour
{
    [SerializeField] private State initalState;
    [SerializeField] private State currentState;

    public void GoTo<T>() where T : State
    {
        State nextState = FindObjectOfType<T>();
        if (currentState != nextState)
        {
            currentState?.Deactivate();
            currentState = nextState;
            Debug.Log("[ STATE ] Transition to " + currentState.GetType().ToString());
            currentState.Activate();
        }
        else
        {
            Debug.LogWarning("[ STATE ] Cannot go to already active state");
        }
    }

    public void GoTo(State nextState)
    {
        if (currentState != nextState)
        {
            currentState?.Deactivate();
            currentState = nextState;
            Debug.Log("[ STATE ] Transition to " + currentState.GetType().ToString());
            currentState.Activate();
        }
        else
        {
            Debug.LogWarning("[ STATE ] Cannot go to already active state");
        }
    }

    private void Start()
    {
        Debug.Log("[ STATE ] Statemachine started");
        GoTo(initalState);
    }
}

