using UnityEngine;

public class BasicTile : TileClass
{

    public GameObject[] initNextTile;
    public GameObject[] initPreviousTile;
    public override GameObject[] nextTile { get { return initNextTile; } }
    public override GameObject[] previousTile { get { return initPreviousTile; } }

    public bool tileOccupied = false;
    


}
