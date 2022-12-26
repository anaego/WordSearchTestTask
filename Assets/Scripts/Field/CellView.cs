using DG.Tweening;
using TMPro;
using UnityEngine;

public class CellView : MonoBehaviour
{
    [SerializeField] private TMP_Text letterText;
    [SerializeField] private float letterFadeInDuration = 0.25f;

    public string Text
    {
        set => letterText.text = value;
    }

    public Tween GetRevealTween()
    {
        return letterText.DOFade(1, letterFadeInDuration);
    }
}
