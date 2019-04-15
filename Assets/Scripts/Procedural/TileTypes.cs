using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TileTypes
{
    public enum BiomeTypes { Desert = 0, Plains = 1, Rock = 2};
    public struct TileInfo
    {
        public bool Created;
        public BiomeTypes Biome;
        public byte[] BiomeWeights;

        public TileInfo(byte[,] previousWeights)
        {
            Created = true;
            BiomeWeights  = new byte[3];

            for (int i = 0; i < previousWeights.GetLength(0); i++)
            {
                for (int j = 0; j < previousWeights.GetLength(1); j++)
                {
                    if (BiomeWeights[j] <= previousWeights[i, j])
                    {
                        BiomeWeights[j] = previousWeights[i, j];
                    }
                }
                
            }

            int biomeIndex = 0;
            for (int o = 0; o < BiomeWeights.GetLength(0); o++)
            {
                if (BiomeWeights[biomeIndex] <= BiomeWeights[o])
                {
                    biomeIndex = o;
                }
            }

            Biome = (BiomeTypes)biomeIndex;
        }
    }
}
