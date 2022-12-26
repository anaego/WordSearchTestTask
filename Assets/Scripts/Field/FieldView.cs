using DG.Tweening;
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
                field[i, j] = cell;
            }
        }
    }

    public void FillField(char[][] data)
    {
        for (int i = 0; i < field.GetLength(0); i++)
        {
            for (int j = 0; j < field.GetLength(1); j++)
            {
                field[i, j].Text = data[i][j].ToString();
            }
        }
    }

    public void RevealWord(int startRow, int startColmn, int endRow, int endColumn)
    {
        Sequence sequence = DOTween.Sequence();
        if (startRow == endRow)
        {
            for (int j = startColmn; j < endColumn; j++)
            {
                sequence.Append(field[startRow, j].GetRevealTween());
            }
        }
        if (startColmn == endColumn)
        {
            for (int i = startRow; i < endRow; i++)
            {
                sequence.Append(field[i, startColmn].GetRevealTween());
            }
        }
        sequence.Play();
    }
}
