using System;
using System.Collections.Generic;
using System.Linq;

public static class StringArrayUtils
{
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

    public static string[] GetFieldRows(char[][] chars)
    {
        List<string> rows = new List<string>();
        foreach (var row in chars)
        {
            rows.Add(new string(row));
        }
        return rows.ToArray();
    }

    public static string[] GetFieldColumns(int rowSize, int columnSize, char[][] wordField)
    {
        List<string> columns = new List<string>();
        var arrayOfColumns = new List<List<char>>();
        for (int c = 0; c < columnSize; c++)
        {
            var column = new List<char>();
            arrayOfColumns.Add(column);
            for (int r = 0; r < rowSize; r++)
            {
                column.Add(wordField[r][c]);
            }
        }
        return StringArrayUtils.GetFieldRows(arrayOfColumns.Select(charList => charList.ToArray()).ToArray());
    }
}
