using System;
using System.Collections;
using System.Collections.Generic;
using Common.CommonScripts;
using DG.Tweening;
using UnityEngine;

public class MainUIItemEffect : FunctionBaseView
{
    public List<RectTransform> mItems = new List<RectTransform>();
    private void Start()
    {
        mBtn1.BtnTxt = "Show";
        mBtn2.BtnTxt = "Hide";
    }
    void ShowAnimation()
    {
        StartCoroutine(Show());
    }
    
    void HideAnimation()
    {
        StartCoroutine(Hide());
    }
    IEnumerator Hide()
    {
        foreach (var item in mItems)
        {
            item.DOLocalMoveX(-100, 1f, false).SetEase(Ease.OutBack);
            yield return new WaitForSeconds(0.02f);
        }
    }
    
    IEnumerator Show()
    {
        foreach (var item in mItems)
        {
            item.DOLocalMoveX(50, 1f, false).SetEase(Ease.OutBack);
            yield return new WaitForSeconds(0.02f);
        }
    }

    protected override void OnBtn1Click()
    {
        base.OnBtn1Click();
        ShowAnimation();
    }

    protected override void OnBtn2Click()
    {
        base.OnBtn2Click();
        HideAnimation();
    }
}
