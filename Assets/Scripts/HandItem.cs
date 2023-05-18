using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

public class HandItem : MonoBehaviour
{
    public Button btn;
    public TextMeshProUGUI btnTxt;

    public void Init(string text, int index, Action<int> callBack)
    {
        btnTxt.text = text;
        gameObject.SetActive(true);
        btn.onClick.AddListener(() =>
        {
            callBack?.Invoke(index);
        });
    }
}
