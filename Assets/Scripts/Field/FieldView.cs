using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldView : MonoBehaviour
{
    [SerializeField] private CellView cellPrefab;
    [SerializeField] private GameObject cellParent;

    private CellView[,] field;

    public void CreateField(int rows, int columns)
    {
        field = new CellView[rows, columns];
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                var cell = Instantiate(cellPrefab, cellParent.transform);
                field[i,j] = cell;
            }
        }
    }

    public void FillField(char[][] data)
    {
        for (int i = 0; i < field.GetLength(0); i++)
        {
            for (int j = 0; j < field.GetLength(1); j++)
            {
                field[i,j].Text = data[i][j].ToString();
            }
        }
    }

    public void RevealWord(int startRow, int startColmn, int endRow, int endColumn)
    {
        if (startRow == endRow)
        {
            for (int j = startColmn; j < endColumn; j++)
            {
                field[startRow, j].IsOpened = true;
            }
        }
        if (startColmn == endColumn)
        {
            for (int i = startRow; i < endRow; i++)
            {
                field[i, startColmn].IsOpened = true;

            }
        }
    }
}
