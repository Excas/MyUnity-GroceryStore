using System;
using System.Collections;
using System.Collections.Generic;
using Common.CommonScripts;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SequenceTrain : FunctionBaseView
{
    public Image mBomb;

    public Transform mTarget1;
    private void Start()
    {
        mBtn1.BtnTxt = "PlayAni";
        mBtn1.AddListener(SequenceAni);
    }

    private void SequenceAni()
    {
        DOTween.Sequence()
            .Append(mBomb.transform.DOLocalPath(GetBezierPos(), 0.6f, PathType.CatmullRom))
            .Append(mBomb.DOColor(Color.red, 0.2f).SetLoops(10, LoopType.Yoyo));
        //.(mBomb.transform.DOScale(new Vector3(1.3f, 1.3f, 1f), 0.04f),1);
    }

    private Vector3[] GetBezierPos()
    {
        int bezierMaxPoint = 30;
        Vector3 bezierOffset = new Vector3();
        Vector3[] posList = new Vector3[bezierMaxPoint];
        Vector3 startPos = mBomb.transform.localPosition;
        Vector3 endPos = mTarget1.localPosition;
        bezierOffset.x = UnityEngine.Random.Range(-200f, 200f);
        bezierOffset.y = (endPos.y - startPos.y) / 2;
        for (int b = 0; b < bezierMaxPoint; b++)
        {
            posList[b] = GetBezier2Pos(startPos, startPos + bezierOffset, endPos, b / (float) bezierMaxPoint);
        }
        return posList;
    }
    
    /// <summary>
    /// 获得贝塞尔曲线坐标
    /// </summary>
    private Vector3 GetBezier2Pos(Vector3 p0, Vector3 p1, Vector3 p2, float t)
    {
        float st = 1 - t;
        return st * st * p0 + 2 * t * st * p1 + t * t * p2;
    }
}
