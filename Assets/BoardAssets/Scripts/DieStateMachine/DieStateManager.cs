using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieStateManager : MonoBehaviour
{
    DieBaseState currentState;
    public DieRollState rollState = new DieRollState();
    public DieMoveState moveState = new DieMoveState();
    public DieDisplayState displayState = new DieDisplayState();

    [HideInInspector]
    public Rigidbody rb;
    [HideInInspector]
    public Transform tr;

    void Start()
    {
        InitializeVariables();
        currentState = rollState;
        currentState.EnterState(this);
    }

    void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(DieBaseState state)
    {
        currentState.ExitState(this);
        currentState = state;
        currentState.EnterState(this);
    }

    public int GetSideFaceUp()
    {
        float _sixVectMag = tr.up.y;
        float _fiveVectMag = tr.forward.y;
        float _threeVectMag = tr.right.y;

        float _absSixVectMag = Mathf.Abs(_sixVectMag);
        float _absFiveVectMag = Mathf.Abs(_fiveVectMag);
        float _absThreeVectMag = Mathf.Abs(_threeVectMag);

        if (_absSixVectMag >= _absFiveVectMag && _absSixVectMag >= _absThreeVectMag)
        {
            if (Mathf.Sign(_sixVectMag) == 1)
                return 6;
            else return 1;
        }
        else if (_absFiveVectMag >= _absSixVectMag && _absFiveVectMag >= _absThreeVectMag)
        {
            if (Mathf.Sign(_fiveVectMag) == 1)
                return 5;
            else return 2;
        }
        else
        {
            if (Mathf.Sign(_threeVectMag) == 1)
                return 3;
            else
                return 4;
        }

    }

    void InitializeVariables()
    {
        rb = GetComponent<Rigidbody>();
        tr = GetComponent<Transform>();
    }
}
