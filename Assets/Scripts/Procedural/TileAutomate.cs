using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileAutomate : MonoBehaviour
{
    public Tile bTile;

    public Tilemap tmWorld;

    private List<List<int>> worldArray;

    // Start is called before the first frame update
    private void Start()
    {
        string input = File.ReadAllText(@"C:\Unity Projects\Scuffed-Odyssey\Assets\Graphics\hell.txt");

        int i = 0, j = 0;

        foreach (var row in input.Split('\n'))
        {
            j = 0;
            foreach (var col in row.Trim().Split(' '))
            {

                worldArray[i][j] = int.Parse(col.Trim());
                j++;
            }
            i++;
        }

//        worldArray = new int[10, 10];
//        worldArray.SetRow(9, 1);
//        worldArray.PrettyPrint2DArray();
        int x = 0, y = 0;
        foreach (var listOfTiles in worldArray)
        {
            x++;
            foreach (var tileValue in listOfTiles)
            {
                y++;
                if (tileValue == 0)
                {
                    tmWorld.SetTile(new Vector3Int(y, -x, 0), bTile);
                }
            }
        }
//        for (int x = 0; x < worldArray.GetLength(0); x++)
//        {
//            for (int y = 0; y < worldArray.GetLength(1); y++)
//            {
//                if (worldArray[x, y] == 1)
//                {
//                    tmWorld.SetTile(new Vector3Int(y, -x, 0), bTile);
//                }
//            }
//        }
//        tmWorld.SetTile(new Vector3Int(0, 0,0), bTile);
    }
}
