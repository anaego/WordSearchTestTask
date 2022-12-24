using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FieldController
{
    public FieldController(FieldView fieldView, DataConfigScriptableObject dataConfig)
    {
        var fieldData = LoadFieldData(dataConfig.WordGridConfigFileName);
    }

    private FieldData LoadFieldData(string wordGridConfigFileName)
    {
        var rawData = Resources.Load<TextAsset>(wordGridConfigFileName);
        if (rawData == null)
        {
            Debug.LogWarning("Couldn't load field data");
            return null;
        }
        char[][] charData = rawData.text.Split("\r\n").Select(stringArray => stringArray.ToCharArray()).ToArray();
        return new FieldData()
        {
            WordGrid = charData
        };
    }
}
