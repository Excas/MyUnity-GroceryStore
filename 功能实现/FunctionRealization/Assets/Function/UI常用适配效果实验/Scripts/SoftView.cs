using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoftView : MonoBehaviour
{
    //适配 1：1~2 范围的宽屏
    public RectTransform rect;
    private void Start()
    {
        SetView();
    }
    public void SetView()
    {
        rect.anchorMin = new Vector2(0.5f, 0);
        rect.anchorMax = new Vector2(0.5f, 1);
        rect.offsetMin = Vector2.zero;
        rect.offsetMax = Vector2.zero;
        rect.sizeDelta = new Vector2(1080, 0);
        rect.pivot = Vector2.one * 0.5f;
    }
}
