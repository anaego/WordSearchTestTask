using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DataConfig", menuName = "ScriptableObjects/DataConfig", order = 1)]
public class DataConfigScriptableObject : ScriptableObject
{
    public string WordGridConfigFileName;
    public string IncorrectInputResponseConfigFileName;

    public int GridSizeRow;
    public int GridSizeColumn;
}
