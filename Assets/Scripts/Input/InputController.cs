using System;
using UnityEngine;

public class InputController
{
    private FieldController fieldController;
    private IncorrectInputResponseData inputData;
    private InputView inputView;

    public InputController(InputView inputView, DataConfigScriptableObject dataConfig, FieldController fieldController)
    {
        this.fieldController = fieldController;
        this.inputView = inputView;
        inputData = LoadInputData(dataConfig.IncorrectInputResponseConfigFileName);
        this.inputView.ButtonAction = ProcessInput;
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

    private void ProcessInput(string word)
    {
        word = word.Replace(" ", String.Empty);
        var coordinates = fieldController.CheckForWord(word);
        if (coordinates == null)
        {
            ProcessIcorrectInput();
        }
        else
        {
            fieldController.RevealWord(coordinates);
        }
    }

    private void ProcessIcorrectInput()
    {
        switch (inputData.ResponseType)
        {
            case IncorrectInputResponseType.PlaySound:
                inputView.PlaySound();
                return;
            case IncorrectInputResponseType.ShakeField:
                inputView.ShakeScreen();
                return;
            case IncorrectInputResponseType.Default:
                return;
        }
    }
}
