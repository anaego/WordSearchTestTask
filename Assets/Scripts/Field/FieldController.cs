using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ModifiedCellData
{
    public char Character;
    public bool IsOpened;

    public ModifiedCellData(char character, bool isOpened = false)
    {
        Character = character;
        IsOpened = isOpened;
    }
}

public class ModifiedFieldData
{
    public ModifiedCellData[,] WordField;

    public ModifiedFieldData(int rows, int columns, char[][] wordField)
    {
        WordField = new ModifiedCellData[rows, columns];
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < columns; c++)
            {
                var modifiedCellData = new ModifiedCellData(wordField[r][c]);
                WordField[r, c] = modifiedCellData;
            }
        }
    }
}

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
                fieldRows = GetFieldRows();
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
                fieldColumns = GetFieldColumns();
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
            foreach (var coordinate in AllCoordinatesOf(FieldRows, word))
            {
                if (!IsRowWordAlreadyFound(coordinate.Item1, coordinate.Item2, coordinate.Item2 + word.Length))
                {
                    return new Tuple<int, int, int, int>
                        (coordinate.Item1, coordinate.Item2, coordinate.Item1, coordinate.Item2 + word.Length);
                }
            }
            return null;
        }
        else
        {
            foreach (var coordinate in AllCoordinatesOf(FieldColumns, word))
            {
                if (!IsColumnWordAlreadyFound(coordinate.Item1, coordinate.Item2, coordinate.Item2 + word.Length))
                {
                    return new Tuple<int, int, int, int>
                        (coordinate.Item2, coordinate.Item1, coordinate.Item2 + word.Length, coordinate.Item1);
                }
            }
            return null;
        }
    }

    // TODO move to a utils
    public static List<Tuple<int, int>> AllCoordinatesOf(string[] strings, string subString)
    {
        List<Tuple<int, int>> coordinates = new List<Tuple<int, int>>();
        for (int i = 0; i < strings.Length; i++)
        {
            for (int j = 0; ; j += subString.Length)
            {
                var index = strings[i].IndexOf(subString, j);
                if (index == -1)
                {
                    break;
                }
                coordinates.Add(Tuple.Create(i, index));
            }
        }
        return coordinates;
    }

    private bool IsRowWordAlreadyFound(int rowIndex, int columnStartIndex, int columnEndIndex)
    {
        for (int c = columnStartIndex; c < columnEndIndex; c++)
        {
            if (modifiedFieldData.WordField[rowIndex, c].IsOpened)
            {
                return true;
            }
        }
        return false;
    }

    private bool IsColumnWordAlreadyFound(int columnIndex, int rowStartIndex, int rowEndIndex)
    {
        for (int r = rowStartIndex; r < rowEndIndex; r++)
        {
            if (modifiedFieldData.WordField[r, columnIndex].IsOpened)
            {
                return true;
            }
        }
        return false;
    }

    internal void RevealWord(Tuple<int, int, int, int> coordinates)
    {
        MarkWordOpened(coordinates.Item1, coordinates.Item2, coordinates.Item3, coordinates.Item4);
        fieldView.RevealWord(coordinates.Item1, coordinates.Item2, coordinates.Item3, coordinates.Item4);
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

    private string[] GetFieldRows(char[][] chars)
    {
        List<string> rows = new List<string>();
        foreach (var row in chars)
        {
            rows.Add(new string(row));
        }
        return rows.ToArray();
    }

    private string[] GetFieldRows()
    {
        return GetFieldRows(fieldData.WordField);
    }

    private string[] GetFieldColumns()
    {
        List<string> columns = new List<string>();
        var arrayOfColumns = new List<List<char>>();
        for (int c = 0; c < dataConfig.FieldSizeColumn; c++)
        {
            var column = new List<char>();
            arrayOfColumns.Add(column);
            for (int r = 0; r < dataConfig.FieldSizeRow; r++)
            {
                column.Add(fieldData.WordField[r][c]);
            }
        }
        return GetFieldRows(arrayOfColumns.Select(charList => charList.ToArray()).ToArray());
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
}
