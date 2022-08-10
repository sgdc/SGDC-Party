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
        Camera[]  _camArray = {rollSelectCam, diceCam};
        foreach (Camera cam in _camArray)
        {
            cam.gameObject.SetActive(false);
        }
    }

}
