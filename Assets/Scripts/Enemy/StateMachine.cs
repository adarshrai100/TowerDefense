using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public BaseState activeState;
    //property for the patrl state.

    // Start is called before the first frame update

    public void Initialise()
    {


        ChangeState(new PatrolState());
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(activeState!=null)
        {
            activeState.Perform();
        }
    }

    public void ChangeState(BaseState newState)
    {
        //check active state
        if(activeState!=null)
        {
            //run clenup on activeState.
            activeState.Exit();
        }
        //change to new state.
        activeState = newState;

        //fil safe null check to make sure new state wasnt null
        if(activeState!=null)
        {
            //setup new state.
            activeState.stateMachine = this;
            //assign state enemy class.
            activeState.enemy = GetComponent<Enemy>();
            activeState.Enter();
        }
    }
}
