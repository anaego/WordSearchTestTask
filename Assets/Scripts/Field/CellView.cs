using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CellView : MonoBehaviour
{
    [SerializeField] private TMP_Text letterText;

    public bool IsOpened { set => letterText.gameObject.SetActive(value); }
}
