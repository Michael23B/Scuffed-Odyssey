using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class WorldBlock
{
    public TileTypes.TileInfo[,] Tile;
    private System.Random _r;
    private int _maxX, _maxY;
    private int[,] _sMod = new int[4, 2] { { -1, 0 }, { 1, 0 }, { 0, -1 }, { 0, 1 } };

    public WorldBlock(int x, int y, int seed)
    {
        _maxX = x;
        _maxY = y;
        _r = new System.Random(seed);
        Tile = new TileTypes.TileInfo[x,y];

        // Init First Tile
        Tile[0, 0].BiomeWeights = new byte[] { 0, 20, 0 };
        Tile[0, 0].Biome = TileTypes.BiomeTypes.Plains;
        Tile[0, 0].Created = true;
    }

    public void SetTile(int x, int y)
    {
        int[,] surroundingXy = modXY(x, y);
        int[] currentWeights = new int[3];

        // Get Surrounding Weights
        for (int i = 0; i < surroundingXy.GetLength(0); i++)
        {
            int modX = surroundingXy[i, 0];
            int modY = surroundingXy[i, 1];
            
            // Get if index is out of bounds
            if (modX < 0 || modY < 0 || modX >= _maxX || modY >= _maxY)
            {
                continue;
            }
            else if (!Tile[modX, modY].Created)
            {
                continue;
            }
            else
            {
                for (int wI = 0; wI < Tile[modX, modY].BiomeWeights.GetLength(0); wI++)
                {
                    currentWeights[wI] += Tile[modX, modY].BiomeWeights[wI];
                }
            }
        }

        // Calculate New Weights
        int maxWeight = 0;
        int maxIndex = 0;

        for (int i = 0; i < currentWeights.GetLength(0); i++)
        {
            if (currentWeights[i] == 0)
            {
                currentWeights[i] = (byte)_r.Next(0, 20);
            }

            if (maxWeight <= currentWeights[i])
            {
                maxWeight = currentWeights[i];
                maxIndex = i;
            }
        }
        if (currentWeights[maxIndex] > 254)
        {
            currentWeights[maxIndex] = 254;
        }

        currentWeights[maxIndex] = (int)(currentWeights[maxIndex] * 0.9);

//        for (int i = 0; i < currentWeights.GetLength(0); i++)
//        {
//            if (i == maxIndex)
//            {
//                continue;
////                currentWeights[i] = Mathf.Clamp(currentWeights[i] - _r.Next(0, 2), 0, 254);
//            }
//
//            currentWeights[i] = Mathf.Clamp(currentWeights[i] + _r.Next(0, 6), 0, 254);
//        }


        // Set Tile
        Tile[x, y].Created = true;
        Tile[x, y].BiomeWeights = new byte[] { (byte)currentWeights[0], (byte)currentWeights[1], (byte)currentWeights[2] };
        Tile[x, y].Biome = (TileTypes.BiomeTypes)maxIndex;
    }

    // TODO:
    public void DeNoise()
    {
        for (int x = 0; x < _maxX; x++)
        {
            for (int y = 0; y < _maxY; y++)
            {
                
            }
        }
    }

    // TODO: Calculate region/chunk stats
    public void CalcRegionStats()
    {

    }

    public int MaxX => _maxX;
    public int MaxY => _maxY;

    private int[,] modXY(int x, int y)
    {
        int[,] moddedXyList = _sMod;
        for (int i = 0; i < _sMod.GetLength(0); i++)
        {
            moddedXyList[i,0] = x + _sMod[i,0];
            moddedXyList[i, 1] = y + _sMod[i, 1];
        }

        return moddedXyList;
    }

}
