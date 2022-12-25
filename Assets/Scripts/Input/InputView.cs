using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputView : MonoBehaviour
{
    [SerializeField] private TMP_InputField input;
    [SerializeField] private Button button;

    public Action<string> ButtonAction 
    { 
        set => button.onClick.AddListener(() => value.Invoke(input.text)); 
    }

    internal void PlaySound()
    {
        
    }

    internal void ShakeScreen()
    {
        
    }
}
