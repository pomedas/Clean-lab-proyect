using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    protected StateMachine stateMachine;

    protected virtual void Awake()
    {
        stateMachine = FindObjectOfType<StateMachine>();
    }

    public void Activate()
    {
        AfterActivate();
    }

    public void Deactivate()
    {
        BeforeDeactivate();
    }

    public abstract void AfterActivate();
    public abstract void BeforeDeactivate();
}
