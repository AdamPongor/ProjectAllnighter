using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Popup : MonoBehaviour
{
    public GameObject PopupBox;
    public TMP_Text PopupText;

    public void Pop(string text)
    {
        PopupText.text = text;
        PopupBox.SetActive(true);
    }

    public void Close()
    {
        PopupBox.SetActive(false);
    }
}
