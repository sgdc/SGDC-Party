using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;



public abstract class TileClass : MonoBehaviour
{

    public abstract GameObject[] nextTile { get; } //throws NullReferenceException on compile, due to it not being initialized by default
    public abstract GameObject[] previousTile { get; }

    public TilePath[] paths = new TilePath[2];// this has a 1-1 ratio with nextTile. so that nextTile[i] == paths[i].endTile

    public int spaceCost = 1; // the cost of walking over the space. this value is decremented from the roll

    public BezierCurve GetPathBezier()
    {
        if (paths.Length == 1)
            return paths[0].GetBezier();
        else/* if (nextTile.Length > 1)*/
            return handleIntersection();
        //else
        //    return Transform.position;
    }

<<<<<<< Updated upstream
        Vector3 handleIntersection()
=======
    BezierCurve handleIntersection()
>>>>>>> Stashed changes
    {  
        return paths[0].GetBezier();
    }

    //inefficient as all hell but im tired so ill fix it later
    //runs when value is changed
    public void OnValidate()
    {
        if(paths.Length > nextTile.Length)
        {
            TilePath[] arr = new TilePath[nextTile.Length];
            for (int j = 0; j < nextTile.Length; j++)
            {
                arr[j] = paths[j];
            }
            paths = arr;
        }

        for (int i = 0; i < nextTile.Length; i++)
        {
            if(nextTile[i] == null)
            {
                continue;
            }
            //creates a new list with len nextTile
            if(paths.Length <= i)
            {
                TilePath[] arr = new TilePath[nextTile.Length];
                for(int j = 0; j < paths.Length; j++)
                {
                    arr[j]=paths[j];
                }
                paths = arr;
            }
            if (paths[i] == null)
            {
                paths[i] = new TilePath(this, nextTile[i].GetComponent<TileClass>());
            }
        }
    }

<<<<<<< Updated upstream
=======
    private void OnDrawGizmosSelected()
    {
        //draws bezier curve and point when selected in the editor
        if (paths.Length > 0)
        {
            for (int i=0;i<paths.Length;i++) { 
                if(paths[i] == null) { continue; }
                GameObject g = nextTile[i];
                Debug.DrawLine(transform.position, paths[i].startBezier, Color.blue);
                Debug.DrawLine(g.transform.position, paths[i].endBezier, Color.blue);
                Gizmos.DrawSphere(paths[i].startBezier, 0.1f);
                Gizmos.DrawSphere(paths[i].endBezier, 0.1f);
                BezierCurve.DrawBezierInDebug(transform.position, g.transform.position,paths[i].startBezier,paths[i].endBezier, 10, Color.green);
            }
        }
    }

    public TilePath GetPath(GameObject endTile)
    {
        for (int i = 0; i < paths.Length; i++)
        {
            if (paths[i].endTile.gameObject == endTile)
            {
                return paths[i];
            }
        }
        return null;
    }
    public TilePath GetPath(int i)
    {
        return paths[i];
    }

    //occurs when the player walks over the tile
    public void OnWalk()
    {

    }

    //occurs when the player lands on the tile (the space they land at)
    public void OnLand()
    {

    }
}

[System.Serializable]public class TilePath{

    public TileClass startTile;
    public TileClass endTile;
    public Vector3 startBezier;
    public Vector3 endBezier;

    public TilePath(TileClass startTile,TileClass endTile)
    {
        this.startTile = startTile;
        this.endTile = endTile;
        startBezier = startTile.transform.position;
        endBezier = endTile.transform.position;
    }

    public BezierCurve GetBezier()
    {
        return new BezierCurve(startTile.transform.position,endTile.transform.position,startBezier,endBezier);
    }
>>>>>>> Stashed changes
}
