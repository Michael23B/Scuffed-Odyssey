using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileAutomate : MonoBehaviour
{
    public Tile bTile;

    public Tilemap tmWorld;

    private List<List<int>> worldArray = new List<List<int>>();

    // Start is called before the first frame update
    private void Start()
    {
        worldArray = new List<List<int>>();
        UpdateMap();
    }

    public void UpdateMap()
    {
        ClearMap();
        string input = File.ReadAllText(@"C:\Users\j_alf\go\src\github.com\trainData\hell.txt");

        int i = 0, j=0;

        foreach (var row in input.Split('\n'))
        {
            worldArray.Add(new List<int>());

            //            j = 0;
            foreach (var col in row.Trim().Split(' '))
            {
                worldArray[i].Add(int.Parse(col.Trim()));
                //                j++;
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

            y = 0;
        }
    }

    public void ClearMap()
    {
        int x = 0, y = 0;
        foreach (var listOfTiles in worldArray)
        {
            x++;
            foreach (var tileValue in listOfTiles)
            {
                y++;
                if (tileValue == 0)
                {
                    tmWorld.SetTile(new Vector3Int(y, -x, 0), null);
                }
            }

            y = 0;
        }
        worldArray = new List<List<int>>();
    }
}
