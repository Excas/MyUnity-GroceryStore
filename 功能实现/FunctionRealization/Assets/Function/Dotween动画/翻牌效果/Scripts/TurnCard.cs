using System;
using System.Collections;
using System.Collections.Generic;
using Common.CommonScripts;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class TurnCard : FunctionBaseView
{
    public GameObject mFront;//卡牌正面
    public GameObject mBack;//卡牌背面
    public CardState mCardState = CardState.Front;//卡牌当前的状态，是正面还是背面？
    public float mTime = 0.3f;
    private bool isActive = false;//true代表正在执行翻转，不许被打断
    
    /// <summary>
    /// 初始化卡牌角度，根据mCardState
    /// </summary>
    public void Init()
    {
        if(mCardState==CardState.Front)
        {
            //如果是从正面开始，则将背面旋转90度，这样就看不见背面了
            mFront.transform.eulerAngles = Vector3.zero;
            mBack.transform.eulerAngles = new Vector3(0, 90, 0);
        }
        else
        {
            //从背面开始，同理
            mFront.transform.eulerAngles = new Vector3(0, 90, 0);
            mBack.transform.eulerAngles = Vector3.zero;
        }
    }
    private void Start()
    {
        Init();
        mBtn1.BtnTxt = "ToBack";
        mBtn2.BtnTxt = "ToFront";
    }
    
    
    protected override void OnBtn1Click()
    {
        base.OnBtn1Click();
        if (isActive)
            return;
        StartCoroutine(ToBack());
    }

    protected override void OnBtn2Click()
    {
        base.OnBtn2Click();
        if (isActive)
            return;
        StartCoroutine(ToFront());
    }
   
    
    IEnumerator ToBack()
    {
        isActive = true;
        mFront.transform.DORotate(new Vector3(0, 90, 0), mTime);
        for (float i = mTime; i >= 0; i -= Time.deltaTime)
            yield return 0;
        mBack.transform.DORotate(new Vector3(0, 0, 0), mTime);
        isActive = false;

    }
   
    
    IEnumerator ToFront()
    {
        isActive = true;
        mBack.transform.DORotate(new Vector3(0, 90, 0), mTime);
        for (float i = mTime; i >= 0; i -= Time.deltaTime)
            yield return 0;
        mFront.transform.DORotate(new Vector3(0, 0, 0), mTime);
        isActive = false;
    }
    
    public enum CardState
    {
        Front,
        Back
    }
}

