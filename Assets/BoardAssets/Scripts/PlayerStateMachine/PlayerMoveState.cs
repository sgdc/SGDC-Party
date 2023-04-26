using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PlayerMoveState : PlayerBaseState
{
    Vector3 _nextTilePos;
<<<<<<< Updated upstream
    Vector3 _moveVector;
    

    public override void EnterState(PlayerStateManager player)
    {
        _nextTilePos = player.currentTile.GetComponent<TileClass>().GetNextTilePos();
        Vector3 _travelVector = _nextTilePos - player.transform.position;
        Vector3 _travelDir = _travelVector.normalized;
        float _spd = _travelVector.magnitude / player.moveTime;
        _moveVector = _spd * _travelDir;
=======
    BezierCurve _currentTileBezier;
    Vector3 _moveVector; //unused in bezier implementation

    //float _spd;
    float _startTime;
    float _timeToCoverDistance;

    public override void EnterState(PlayerStateManager player)
    {
        Debug.Log("EntersState");
        _currentTileBezier = player.currentTile.GetComponent<TileClass>().GetPathBezier();
        _timeToCoverDistance = _currentTileBezier.GetLength(5) / player.moveTime;
        _startTime = Time.time;
>>>>>>> Stashed changes
    }

    public override void UpdateState(PlayerStateManager player)
    {
        if(player.toMove > 0)
        {
<<<<<<< Updated upstream
            if ((_nextTilePos - player.transform.position).magnitude <= (_moveVector * Time.deltaTime).magnitude)
            {
                Debug.Log("toMove = " + player.toMove + ", currentTile = " + player.currentTile);
                player.transform.position = _nextTilePos;
                player.currentTile = player.currentTile.GetComponent<TileClass>().nextTile[0];
                player.SwitchState(player.idleState);
                if(player.currentTile.GetComponent<TileClass>().previousTile[0].GetComponent<BasicTile>().tileOccupied)
                {
                    player.currentTile.GetComponent<TileClass>().previousTile[0].transform.GetChild(0).GetComponent<Renderer>().material  = (Material)AssetDatabase.LoadAssetAtPath("Assets/BoardAssets/Materials/TestTileUnoccupiedMat.mat", typeof(Material));
                }
                player.currentTile.GetComponent<BasicTile>().tileOccupied = true;
                player.currentTile.transform.GetChild(0).GetComponent<Renderer>().material = (Material)AssetDatabase.LoadAssetAtPath("Assets/BoardAssets/Materials/TestTileOccupiedMat.mat", typeof(Material));
                player.toMove-=1;
            }
            else
            {

                player.transform.position += _moveVector * Time.deltaTime;
=======
            if ((Time.time - _startTime + Time.deltaTime) / _timeToCoverDistance > 1)
            {
                player.transform.position = _nextTilePos;
                player.currentTile = player.currentTile.GetComponent<TileClass>().nextTile[0];
                player.SwitchState(player.idleState);

                //I don't believe this is ever set to false or accessed
                //player.currentTile.GetComponent<BasicTile>().tileOccupied = true;

                //calculates the time to cross the curve
                _currentTileBezier = player.currentTile.GetComponent<TileClass>().GetPathBezier();
                _timeToCoverDistance = _currentTileBezier.GetLength(5) / player.moveTime;
                _startTime = Time.time;

                player.toMove-=player.currentTile.GetComponent<TileClass>().spaceCost; // decrements the space cost of the individual tile
            }
            else
            {
                //moves to position on bezier curve
                player.transform.position = _currentTileBezier.GetPosition(Mathf.Clamp((Time.time - _startTime) / _timeToCoverDistance, 0, 1));
                //rotates it in the moving direction
                Vector3 lookDirection = player.transform.position - _currentTileBezier.GetPosition(Mathf.Clamp((Time.time - _startTime + Time.deltaTime) / _timeToCoverDistance, 0, 1));
                player.transform.rotation = Quaternion.LookRotation(lookDirection);
>>>>>>> Stashed changes
            }
            
        }

    }
    public override void ExitState(PlayerStateManager player)
    {

    }
}
