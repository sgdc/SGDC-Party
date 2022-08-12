using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraStateManager : MonoBehaviour
{
    CameraBaseState currentState;
    public CameraIdleState idleState = new CameraIdleState();
    public CameraMoveState moveState = new CameraMoveState();

    [HideInInspector]
    public Camera currentCamera;
    [HideInInspector]
    public Camera nextCamera;

    public Camera diceCam;
    public Camera rollSelectCam;

    private Camera[] _camArray;

    void Start()
    {
        InitializeVariables();

        // Setting our current state and initial camera, then setting those two to be the active state/camera

        currentState = idleState;
        currentCamera = diceCam;

        SwitchCamera(currentCamera);
        currentState.EnterState(this);
    }

    void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(CameraBaseState state)
    {
        currentState.ExitState(this);
        currentState = state;
        currentState.EnterState(this);
    }

    public void TransitionCam(Camera cam)
    {
        currentState.ExitState(this);
        Debug.Log("Cam Exited State");
        currentState = moveState;
        nextCamera = cam;
        currentState.EnterState(this);
    }

    public void SwitchCamera(Camera targetCam)
    {
        currentCamera.gameObject.SetActive(false);
        targetCam.gameObject.SetActive(true);
    }

    private void InitializeVariables()
    {
        //Here, we get a list of all cameras in the scene and set them to the inactive state
        Camera[]  _camArray = {rollSelectCam, diceCam};
        foreach (Camera cam in _camArray)
        {
            cam.gameObject.SetActive(false);
        }
    }

}
