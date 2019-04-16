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
    private static readonly int xySize = 50;
    private int[,] worldArray = new int[xySize, xySize];

    public TileTypes.TileInfo[,] tileOfHell = new TileTypes.TileInfo[xySize, xySize];
    private System.Random r = new System.Random();

    // Start is called before the first frame update
    private void Start()
    {
        int[,] sOfHell = new int[4,2]{{-1,0},{1,0},{0,-1},{0,1}};

        tileOfHell[0,0].BiomeWeights = new byte[]{0,20,0};
        tileOfHell[0, 0].Biome = TileTypes.BiomeTypes.Plains;
        for (int x = 0; x < tileOfHell.GetLength(0); x++)
        {
            for (int y = 0; y < tileOfHell.GetLength(1); y++)
            {
                // Check Surroundings
                int[] currentWeights = new int[3];
                for (int i = 0; i < sOfHell.GetLength(0); i++)
                {
                    int modX = sOfHell[i,0] + x;
                    int modY = sOfHell[i, 1] + y;

                    if (modX < 0 || modY < 0 || modX >= xySize || modY >= xySize)
                    {
                        continue;
                    }
                    else if (!tileOfHell[modX,modY].Created)
                    {
                        continue;
                    }
                    else
                    {
                        for (int wI = 0; wI < tileOfHell[modX, modY].BiomeWeights.GetLength(0); wI++)
                        {
                            currentWeights[wI] += tileOfHell[modX, modY].BiomeWeights[wI];
                        }
                    }
                }

                int maxWeight = 0;
                int maxIndex = 0;
                for (int i = 0; i < currentWeights.GetLength(0); i++)
                {
                    if (currentWeights[i] == 0)
                    {
                        currentWeights[i] = (byte)r.Next(0, 30);
                    }

                    if (maxWeight <= currentWeights[i])
                    {
                        maxWeight = currentWeights[i];
                        maxIndex = i;
                    }
                }
                if  (currentWeights[maxIndex] > 254)
                {
                    currentWeights[maxIndex] = 254;
                }

                currentWeights[maxIndex] = (int)(currentWeights[maxIndex]*0.9);

                // Tile
                tileOfHell[x, y].Created = true;
                tileOfHell[x, y].BiomeWeights = new byte[]{(byte)currentWeights[0], (byte)currentWeights[1], (byte)currentWeights[2] };
                tileOfHell[x, y].Biome = (TileTypes.BiomeTypes) maxIndex;

                tmWorld.SetTile(new Vector3Int(y, -x, 0), bTile[maxIndex]);
                
            }
        }

            //        worldArray = new int[10, 10];
            //        worldArray.SetRow(9, 1);
            //        worldArray.PrettyPrint2DArray();
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
