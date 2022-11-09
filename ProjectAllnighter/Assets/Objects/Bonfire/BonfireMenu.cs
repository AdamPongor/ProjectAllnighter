using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BonfireMenu : MonoBehaviour
{
    public TMP_Text placeName;
    public void SetName(string name)
    {
        placeName.text = name;
    }
}
