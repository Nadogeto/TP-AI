﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using State;

public class AI : MonoBehaviour
{
    public bool switchState = false;
    public float gameTimer;
    public int seconds = 0;

    public StateMachine<AI> stateMachine { get; set; }

    private void Start()
    {
        stateMachine = new StateMachine<AI>(this);
        stateMachine.ChangeState(FirstState.Instance);
        gameTimer = Time.time;
    }

    //appelle l'Update de stateMachine toutes les 5secondes
    private void Update()
    {
        if(Time.time > gameTimer + 1)
        {
            gameTimer = Time.time;
            seconds++;
            Debug.Log(seconds);
        }

        if(seconds == 5)
        {
            seconds = 0;
            switchState = !switchState;
        }

        stateMachine.Update();
    }

}
