using UnityEngine;

public class DieMoveState : DieBaseState
{

    float _distFromCam = 15, _dieSpacing = 2;

    Quaternion  _rotInit;

    float _t, _time, _transitionTime = 3;

    Vector3 _targetPosition;

    Vector3 _rotTarget, _faceUpDir;
    public override void EnterState(DieStateManager die)
    {
        SetPhysics(die);
        SetDestination(die);
        SetTargetRot(die);
        SetTimer(die);
    }

    public override void UpdateState(DieStateManager die)
    {
        Rotate(die);
        Move(die);
        Timer(die);
    }

    public override void ExitState(DieStateManager die)
    {

    }

    void SetDestination(DieStateManager die)
    {
        _targetPosition = die.rollSelectCam.transform.position + die.rollSelectCam.transform.forward * _distFromCam - die.rollSelectCam.transform.right * ((die.numDice - 1) / 2) * _dieSpacing;
        _targetPosition += die.rollSelectCam.transform.right * die.ID * _dieSpacing;
    }
    void SetTargetRot(DieStateManager die)
    {
        Debug.Log("Die "+die.ID+" value is: "+die.GetSideFaceUp());
        switch (die.GetSideFaceUp())
        {
            case 1:
                _faceUpDir = -die.transform.up;
                _rotTarget = new Vector3(90, 0, 0);
                break;
            case 2:
                _faceUpDir = -die.transform.forward;
                _rotTarget = new Vector3(0, 0, 0);
                break;
            case 3:
                _faceUpDir = die.transform.right;
                _rotTarget = new Vector3(0, 90, 0);
                break;
            case 4:
                _faceUpDir = -die.transform.right;
                _rotTarget = new Vector3(0, 270, 0);
                break;
            case 5:
                _faceUpDir = die.transform.forward;
                _rotTarget = new Vector3(180, 0, 0);
                break;
            case 6:
                _faceUpDir = die.transform.up;
                _rotTarget = new Vector3(270, 0, 0);
                break;
        }
        Debug.Log("Die "+die.ID+"'s faceUpDir is "+_faceUpDir);
        _rotInit = die.transform.rotation;
        Debug.Log("Die "+die.ID+"'s _rotInit is "+_rotInit);
        Debug.Log("Die "+die.ID+" should rotate from "+_rotInit+" to "+_rotTarget);

    }
    void Rotate(DieStateManager die)
    {
        die.transform.eulerAngles = _rotTarget; //Quaternion.Slerp(_rotInit, _rotTarget, _t);
    }

    void Move(DieStateManager die)
    {
        Vector3 _linDist = _targetPosition - die.transform.position;
        Vector3 _scaledDist = _linDist * Time.deltaTime / _time;
        Vector3.Project(_scaledDist, _faceUpDir);
        die.transform.position += _scaledDist;
    }
    void Timer(DieStateManager die)
    {
        _time -= Time.deltaTime;
        if (_time <= 0)
            die.SwitchState(die.displayState);
        _t = (_transitionTime - _time) / _transitionTime;
    }
    void SetTimer(DieStateManager die)
    {
        _time = _transitionTime;
    }
    void SetPhysics(DieStateManager die)
    {
        die.rb.useGravity = false;
        die.GetComponent<BoxCollider>().enabled = false;
    }
}
