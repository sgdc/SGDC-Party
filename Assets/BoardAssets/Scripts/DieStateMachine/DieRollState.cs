using UnityEngine;

public class DieRollState : DieBaseState
{



    public float maxInitHorVel = 3;
    public float minInitVertVel = 12;
    public float maxInitVertVel = 7;

    public float maxInitAngVel = 60;

    public float countDown = 3;

    public int faceUpSide;

    public override void EnterState(DieStateManager die)
    { 
        SetRandomValues(die);
    }

    public override void UpdateState(DieStateManager die)
    {

    }
    public override void ExitState(DieStateManager die)
    {

    }

    void SetRandomValues(DieStateManager die)
    {
        die.rb.velocity = new Vector3(Random.Range(-maxInitHorVel, maxInitHorVel), Random.Range(minInitVertVel, maxInitVertVel), Random.Range(-maxInitHorVel, maxInitHorVel));
        die.rb.angularVelocity = new Vector3(Random.Range(-maxInitAngVel, maxInitAngVel), Random.Range(-maxInitAngVel, maxInitAngVel), Random.Range(-maxInitAngVel, maxInitAngVel));
    }

}
