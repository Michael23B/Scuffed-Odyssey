using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileAutomate : MonoBehaviour
{
    public Tile[] bTile;

    public Tilemap tmWorld;
    public WorldBlock worldBlock;

    // Start is called before the first frame update
    private void Start()
    {
        worldBlock = new WorldBlock(25,25,65456465);
        for (int x = 0; x < worldBlock.MaxX; x++)
        {
            for (int y = 0; y < worldBlock.MaxY; y++)
            {
                if (!(x == 0 && y == 0)) {
                worldBlock.SetTile(x,y);
                }
                tmWorld.SetTile(new Vector3Int(y, -x, 0), bTile[(int)worldBlock.Tile[x,y].Biome]);
            }
        }

    }
}
