using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LayoutGroupView : MonoBehaviour
{
    public Slider mL1;
    public LayoutElement mLayout1;
    private float mOriginMinHeight1;

    
    public Slider mL2;
    public RectTransform rect2;
    private float mOriginRectHeight2;
    private void Start()
    {
        mL1.onValueChanged.AddListener(Slider1Change);
        mOriginMinHeight1 = mLayout1.minHeight;
        
        mL2.onValueChanged.AddListener(Slider2Change);
        mOriginRectHeight2 = rect2.sizeDelta.y;
    }

    private void Slider1Change(float value)
    {
        mLayout1.gameObject.SetActive(value >= 0.1f);
        mLayout1.minHeight = mOriginMinHeight1 + value * 300;
    }
    
    private void Slider2Change(float value)
    {
        rect2.sizeDelta = new Vector2(rect2.sizeDelta.x,mOriginRectHeight2 + value * 300);
    }
}
