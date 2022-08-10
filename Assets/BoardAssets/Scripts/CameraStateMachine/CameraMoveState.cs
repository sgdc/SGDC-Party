using UnityEngine;

public class CameraMoveState : CameraBaseState
{

    float _transitionTime = 3;
    float _time;
    float _t;
    Quaternion _rotInit;
    Quaternion _rotTarget;
    Camera _tempCam;
    float _FOVTarget;
    float _FOVInit;

    public override void EnterState(CameraStateManager cam)
    {
        InitializeVariables(cam);
        Debug.Log("Cam Init Vars");
        cam.SwitchCamera(_tempCam);
        Debug.Log("Cam Switched to temp cam");
    }
    public override void UpdateState(CameraStateManager cam)
    {
        Move(cam);
        Rotate(cam);
        FOV(cam);
        Timer(cam);
    }
    public override void ExitState(CameraStateManager cam)
    {
        cam.SwitchCamera(cam.nextCamera);
        cam.currentCamera = cam.nextCamera;
    }


    void Rotate(CameraStateManager cam)
    {
        _tempCam.transform.rotation = Quaternion.Slerp(_rotTarget, _rotInit, _t);
    }

    void Move(CameraStateManager cam)
    {
        Vector3 _linDist = cam.nextCamera.transform.position - _tempCam.transform.position;
        Vector3 _scaledDist = _linDist * Time.deltaTime / _time;
        Vector3.Project(_scaledDist, _tempCam.transform.forward);
        _tempCam.transform.position += _scaledDist;
    }

    void InitializeVariables(CameraStateManager cam)
    {
        _tempCam = Camera.Instantiate(cam.currentCamera);
        _rotInit = cam.currentCamera.transform.rotation;
        _rotTarget = cam.nextCamera.transform.rotation;
        _time = _transitionTime;
        _FOVInit = cam.currentCamera.fieldOfView;
        _FOVTarget = cam.nextCamera.fieldOfView;
    }

    void FOV(CameraStateManager cam)
    {
        _tempCam.fieldOfView = Mathf.Lerp(_FOVTarget, _FOVInit, _t);
    }
    void Timer(CameraStateManager cam)
    {
        _time -= Time.deltaTime;
        if (_time <= 0)
            cam.SwitchState(cam.idleState);
        _t = _time / _transitionTime;
    }

}
