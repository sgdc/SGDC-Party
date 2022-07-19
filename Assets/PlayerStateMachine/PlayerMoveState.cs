using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerBaseState
{
    Vector3 _nextTilePos;
    Vector3 _moveVector;

    public override void EnterState(PlayerStateManager player)
    {
        _nextTilePos = player.currentTile.GetComponent<TileClass>().GetNextTilePos();
        Vector3 _travelVector = _nextTilePos - player.transform.position;
        Vector3 _travelDir = _travelVector.normalized;
        float _spd = _travelVector.magnitude / player.moveTime;
        _moveVector = _spd * _travelDir;
    }

    public override void UpdateState(PlayerStateManager player)
    {
        if ((_nextTilePos - player.transform.position).magnitude <= (_moveVector * Time.deltaTime).magnitude)
        {
            player.transform.position = _nextTilePos;
            player.currentTile = player.currentTile.GetComponent<TileClass>().nextTile[0];
            player.SwitchState(player.idleState);
        }
        else
            player.transform.position += _moveVector * Time.deltaTime;
    }
    public override void ExitState(PlayerStateManager player)
    {

    }

    private void MoveToNextTile(PlayerStateManager player)
    {
        
    }

}
