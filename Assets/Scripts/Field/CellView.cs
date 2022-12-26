using DG.Tweening;
using TMPro;
using UnityEngine;

public class CellView : MonoBehaviour
{
    [SerializeField] private TMP_Text letterText;
    [SerializeField] private float letterFadeInDuration = 0.5f;

    public string Text
    {
        set => letterText.text = value;
    }
    public bool IsOpened
    {
        set
        {
            letterText.alpha = 0;
            letterText.gameObject.SetActive(value);
            letterText.DOFade(1, letterFadeInDuration);
        }
    }
}
