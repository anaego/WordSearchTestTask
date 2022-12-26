using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FieldController
{
    private FieldView fieldView;
    private FieldData fieldData;
    private DataConfigScriptableObject dataConfig;
    private ModifiedFieldData modifiedFieldData;
    private string[] fieldRows;
    private string[] fieldColumns;

    public string[] FieldRows
    {
        get
        {
            if (fieldRows == null)
            {
                fieldRows = StringArrayUtils.GetFieldRows(fieldData.WordField);
            }
            return fieldRows;
        }
    }

    public string[] FieldColumns
    {
        get
        {
            if (fieldColumns == null)
            {
                fieldColumns = StringArrayUtils.GetFieldColumns(
                    dataConfig.FieldSizeRow, dataConfig.FieldSizeColumn, fieldData.WordField);
            }
            return fieldColumns;
        }
    }

    public FieldController(FieldView fieldView, DataConfigScriptableObject dataConfig)
    {
        this.fieldView = fieldView;
        this.dataConfig = dataConfig;
        fieldData = LoadFieldData(dataConfig.FieldConfigFileName);
        fieldView.CreateField(dataConfig.FieldSizeRow, dataConfig.FieldSizeColumn);
        fieldView.FillField(fieldData.WordField);
        modifiedFieldData = new ModifiedFieldData(dataConfig.FieldSizeRow, dataConfig.FieldSizeColumn, fieldData.WordField);
    }

    public Tuple<int, int, int, int> CheckForWord(string word)
    {
        var wordIsInARow = FieldRows.Any(row => row.Contains(word));
        var wordIsInAColumn = FieldColumns.Any(column => column.Contains(word));
        if (!wordIsInARow && !wordIsInAColumn)
        {
            return null;
        }
        if (wordIsInARow)
        {
            foreach (var coordinate in StringArrayUtils.AllCoordinatesOf(FieldRows, word))
            {
                if (!IsWordAlreadyOpened(coordinate.Item1, coordinate.Item2, coordinate.Item1, coordinate.Item2 + word.Length))
                {
                    return new Tuple<int, int, int, int>
                        (coordinate.Item1, coordinate.Item2, coordinate.Item1, coordinate.Item2 + word.Length);
                }
            }
            return null;
        }
        else
        {
            foreach (var coordinate in StringArrayUtils.AllCoordinatesOf(FieldColumns, word))
            {
                if (!IsWordAlreadyOpened(coordinate.Item2, coordinate.Item1, coordinate.Item2 + word.Length, coordinate.Item1))
                {
                    return new Tuple<int, int, int, int>
                        (coordinate.Item2, coordinate.Item1, coordinate.Item2 + word.Length, coordinate.Item1);
                }
            }
            return null;
        }
    }

    public void RevealWord(Tuple<int, int, int, int> coordinates)
    {
        MarkWordOpened(coordinates.Item1, coordinates.Item2, coordinates.Item3, coordinates.Item4);
        fieldView.RevealWord(coordinates.Item1, coordinates.Item2, coordinates.Item3, coordinates.Item4);
    }

    public void MarkWordOpened(int startRow, int startColmn, int endRow, int endColumn)
    {
        if (startRow == endRow)
        {
            for (int j = startColmn; j < endColumn; j++)
            {
                modifiedFieldData.WordField[startRow, j].IsOpened = true;
            }
        }
        if (startColmn == endColumn)
        {
            for (int i = startRow; i < endRow; i++)
            {
                modifiedFieldData.WordField[i, startColmn].IsOpened = true;

            }
        }
    }

    private FieldData LoadFieldData(string wordFieldConfigFileName)
    {
        var rawData = Resources.Load<TextAsset>(wordFieldConfigFileName);
        if (rawData == null)
        {
            Debug.LogWarning("Couldn't load field data");
            return null;
        }
        char[][] charData = rawData.text.Split("\r\n").Select(stringArray => stringArray.ToCharArray()).ToArray();
        return new FieldData()
        {
            WordField = charData
        };
    }

    private bool IsWordAlreadyOpened(int startRow, int startColmn, int endRow, int endColumn)
    {
        if (startRow == endRow)
        {
            for (int c = startColmn; c < endColumn; c++)
            {
                if (modifiedFieldData.WordField[startRow, c].IsOpened)
                {
                    return true;
                }
            }
        }
        if (startColmn == endColumn)
        {
            for (int r = startRow; r < endRow; r++)
            {
                if (modifiedFieldData.WordField[r, startColmn].IsOpened)
                {
                    return true;
                }
            }
        }
        return false;
    }
}
