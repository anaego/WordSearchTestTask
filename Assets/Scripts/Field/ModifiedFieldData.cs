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
