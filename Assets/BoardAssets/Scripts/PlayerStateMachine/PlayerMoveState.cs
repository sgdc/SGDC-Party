using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

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
        if(player.toMove > 0)
        {
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
            }
            
        }

    }
    public override void ExitState(PlayerStateManager player)
    {

    }

    private void MoveToNextTile(PlayerStateManager player)
    {
        
    }

}
