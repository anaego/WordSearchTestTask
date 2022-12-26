using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputView : MonoBehaviour
{
    [SerializeField] private TMP_InputField input;
    [SerializeField] private Button button;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private RectTransform shakeAnimationTarget;
    [SerializeField] private float shakeDuration = 1;

    private Tween shakeTween;

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
        if (shakeTween == null || !shakeTween.IsActive())
        {
            shakeTween = shakeAnimationTarget.DOShakeRotation(shakeDuration);
        }
        else
        {
            shakeTween.Rewind();
            shakeTween.Play();
        }
    }
}
