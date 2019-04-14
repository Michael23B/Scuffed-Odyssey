using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA;

public static class UtilityMethods
{
    // Initializes every element of an array to the provided value
    public static T[] InitializeArray<T>(this T[] arr, T value)
    {
        for (int i = 0; i < arr.Length; ++i)
        {
            arr[i] = value;
        }

        return arr;
    }

    // Initializes every element of a 2D array to the provided value
    // int[,]  int, WorldArray type[,] WorldArray, int: 0
    public static T[,] Initialize2DArray<T>(this T[,] arr, T value)
    {
        for (int i = 0; i < arr.GetLength(0); ++i)
        {
            for (int j = 0; j < arr.GetLength(1); ++j)
            {
                arr[i, j] = value;
            }
        }

        return arr;
    }

    public static T[,] SetRow<T>(this T[,] arr, int row, T value)
    {
        for(int i = 0; i < arr.GetLength(0); ++i)
        {
            if (i == row)
            {
                for (int j = 0; j < arr.GetLength(1); ++j)
                {
                    arr[i, j] = value;
                }

                break;
            }
        }

        return arr;
    }

    public static void PrettyPrint2DArray<T>(this T[,] matrix)
    {
        string msgLine = "Printing 2D Array\nCLICK TO EXPAND\n{ ";
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                msgLine = $"{msgLine}{matrix[i, j]}, ";
//                Debug.Log(matrix[i, j] + "\t");
            }

            msgLine = msgLine.TrimEnd(',', ' ');
            msgLine = $"{msgLine} }}\n{{ ";
        }

            Debug.Log(msgLine.TrimEnd('{', ' ')+ "\n");
    }
}


