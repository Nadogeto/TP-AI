using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using State;

public class FirstState : State<AI>
{
    private static FirstState _instance;

    private FirstState()
    {
        if(_instance != null)
        {
            return;
        }

        _instance = this;
    }

    public static FirstState Instance
    {
        get
        {
            if (_instance == null)
            {
                new FirstState();
            }

            return _instance;
        }
    }

    public override void EnterState(AI _owner)
    {
        Debug.Log("Entering first state");
    }

    public override void ExitState(AI _owner)
    {
        Debug.Log("Exiting first state");
    }

    public override void UpdateState(AI _owner)
    {
        if(_owner.switchState)
        {
            _owner.stateMachine.ChangeState(SecondState.Instance);
        }
    }
}
