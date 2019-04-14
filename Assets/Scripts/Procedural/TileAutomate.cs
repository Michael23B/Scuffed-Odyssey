using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileAutomate : MonoBehaviour
{
    public Tile bTile;

    public Tilemap tmWorld;

    private int[,] worldArray = new int[10, 10];

    // Start is called before the first frame update
    private void Start()
    {
        worldArray = new int[10, 10];
        worldArray.SetRow(9, 1);
        worldArray.PrettyPrint2DArray();
        for (int x = 0; x < worldArray.GetLength(0); x++)
        {
            for (int y = 0; y < worldArray.GetLength(1); y++)
            {
                if (worldArray[x, y] == 1)
                {
                    tmWorld.SetTile(new Vector3Int(y, -x, 0), bTile);
                }
            }
        }
//        tmWorld.SetTile(new Vector3Int(0, 0,0), bTile);
    }
}
