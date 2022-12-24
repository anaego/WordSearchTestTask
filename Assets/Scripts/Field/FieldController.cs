using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FieldController
{
    private DataConfigScriptableObject dataConfig;

    public FieldController(FieldView fieldView, DataConfigScriptableObject dataConfig)
    {
        this.dataConfig = dataConfig;
        var fieldData = LoadFieldData();
        Debug.Log("");
    }

    private FieldData LoadFieldData()
    {
        var rawData = Resources.Load<TextAsset>(dataConfig.WordGridConfigFileName);
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
