using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.FilePathAttribute;

public class Matrix : MonoBehaviour
{

    public Vector3[] vectors;
    public Vector3[,] vectors2;
    public Matrix matrix;
    public Matrix4x4 matrix2;

    public enum colors { red, blue, green, yellow, cyan, white, purple };
    public int rows = 7;
    public int column = 4;
    public colors[,] blockColors;
    public int[] intArray;
    public int[,] intArray2;
    public int[][] intArray3;
    private void Awake()
    {
        blockColors = new colors[rows, column];
    }
}
