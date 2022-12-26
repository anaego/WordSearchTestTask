using System;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class InputView : MonoBehaviour
{
    [SerializeField] private TMP_InputField input;
    [SerializeField] private Button button;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private RectTransform shakeAnimationTarget;
    [SerializeField] private float shakeDuration = 1;

    public Action<string> ButtonAction 
    { 
        set => button.onClick.AddListener(() => value.Invoke(input.text)); 
    }

    internal void PlaySound()
    {
        audioSource.Play();
    }

    internal void ShakeScreen()
    {
        shakeAnimationTarget.DOShakeRotation(shakeDuration);
    }
}
