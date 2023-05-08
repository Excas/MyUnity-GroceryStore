using System.Collections;
using System.Collections.Generic;
using Common.CommonScripts;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpSlider : FunctionBaseView
{
    public Slider mSlider;

    private Sequence mScoreSequence;

    private void Start()
    {
        mSlider.maxValue = 1;

        mBtn1.BtnTxt = "Ani";
    }

    protected override void OnBtn1Click()
    {
        base.OnBtn1Click();
        LevelUpParam param = new LevelUpParam
        {
            FromValue = 0.1f,
            ToValue = 0.88f,
            LevelUp = 5,
        };
        mSlider.value = param.FromValue;
        LevelUpAni(param);
    }


    private void LevelUpAni(LevelUpParam param)
    {
        if (mScoreSequence != null)
        {
            mScoreSequence.Kill();
        }

        mScoreSequence = DOTween.Sequence();

        if (param.LevelUp > 0)
        {
            mScoreSequence.Append(DOTween.To(SliderAni, param.FromValue, 1, 1));
            if (param.LevelUp > 1)
            {
                mScoreSequence.Append(DOTween.To(SliderAni, 0, 1, 0.5f).SetLoops(param.LevelUp - 1));
            }

            mScoreSequence.Append(DOTween.To(SliderAni, 0, param.ToValue, 1));

        }
        else
        {
            mScoreSequence.Append(DOTween.To(SliderAni, param.FromValue, param.ToValue, 1));
        }

        mScoreSequence.OnComplete(AniComplete);
    }

    private void AniComplete()
    {
        Debug.Log("SliderAniFinish!!!");
    }

    private void SliderAni(float value)
    {
        mSlider.value = value;
    }
}

public struct LevelUpParam
{
    //初始值
    public float FromValue;
    //目标值
    public float ToValue;
    //升了多少级
    public int LevelUp;
}

