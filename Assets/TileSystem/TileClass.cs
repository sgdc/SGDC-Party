using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public abstract class TileClass : MonoBehaviour
{

    public abstract GameObject[] nextTile { get; }
    public abstract GameObject[] previousTile { get; }

    public Vector3 GetNextTilePos()
    {
        if (nextTile.Length == 1)
            return nextTile[0].transform.position;
        else/* if (nextTile.Length > 1)*/
            return handleIntersection();
        //else
        //    return Transform.position;
    }

        Vector3 handleIntersection()
    {  
        return Vector3.up;
    }

}
