using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController
{
    public InputController(InputView inputView, DataConfigScriptableObject dataConfig)
    {
        var inputData = LoadInputData(dataConfig.IncorrectInputResponseConfigFileName);
    }

    private IncorrectInputResponseData LoadInputData(string incorrectInputResponseConfigFileName)
    {
        var rawData = Resources.Load<TextAsset>(incorrectInputResponseConfigFileName);
        if (rawData == null)
        {
            Debug.LogWarning("Couldn't load input data");
            return null;
        }
        Enum.TryParse<IncorrectInputResponseType>(rawData.text, out var responseType);
        return new IncorrectInputResponseData()
        {
            ResponseType = responseType
        };
    }
}
