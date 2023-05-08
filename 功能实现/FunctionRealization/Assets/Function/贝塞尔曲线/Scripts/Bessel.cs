using System;
using System.Collections.Generic;
using Common.CommonScripts;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public partial class Bessel
{
    /// <summary>
    /// 获取贝塞尔曲线
    /// </summary>
    /// <param name="startPos"></param>
    /// <param name="endPos"></param>
    /// <returns></returns>
    public static Vector3[] GetBezier(Vector3 startPos, Vector3 endPos)
    {
        int bezierMaxPoint = 30;
        Vector3 bezierOffset = new Vector3();
        Vector3[] posList = new Vector3[bezierMaxPoint];
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
    public static Vector3 GetBezier2Pos(Vector3 p0, Vector3 p1, Vector3 p2, float t)
    {
        float st = 1 - t;
        return st * st * p0 + 2 * t * st * p1 + t * t * p2;
    }
}
public partial class Bessel : FunctionBaseView
{
    public GameObject mTarget;
    public Slider mSlider;
    public TextMeshProUGUI mAddTipTxt;
    public GameObject mFlyItem;
    public Transform mViewRoot;

    //延迟移动时间、每块出发间隔时间
    private const float FlyItemDelayTime = 0.08f;
    //飞行总时间
    private const float FlyItemFlyTimeTotal = 0.6f;
    //半个对象池
    private static Dictionary<int, List<Transform>> mItemPool = new Dictionary<int, List<Transform>>();

    private float mSliderValue;

    private void Start()
    {
        mBtn1.BtnTxt = "Fly";
    }

    protected override void OnBtn1Click()
    {
        base.OnBtn1Click();
        SliderEffect();
    }

    private void SliderEffect()
    {
        long flyCount = UnityEngine.Random.Range(5, 12);
        Vector3 endPos = mViewRoot.transform.InverseTransformPoint(mTarget.transform.position);
        mSliderValue += 0.2f;
        mSliderValue = mSliderValue > 1 ? 0 : mSliderValue;
        FlyItems(flyCount, mFlyItem, mViewRoot, Vector3.zero, endPos, null, () =>
        {
            //飞完道具之后的操作
            ShowAddActPointTip(flyCount);
            DoHeadAni(flyCount);
            DoSliderAni(mSliderValue);
        });
    }

    private void DoSliderAni(float time)
    {
        mSlider.DOValue(time, 1);
    }

    private void DoHeadAni(long shakeNum)
    {
        var sequence = DOTween.Sequence();
        sequence.Append(mTarget.transform.DOScale(new Vector3(1.3f, 1.3f, 1f), 0.04f))
            .AppendCallback(PlayGoldFlyEndSound).SetLoops((int) shakeNum, LoopType.Yoyo).OnComplete((() =>
            {
                mTarget.transform.localScale = Vector3.one;
            }));
    }

    private void PlayGoldFlyEndSound()
    {
        Debug.Log("播放飞行之后音效~");
    }

    private void ShowAddActPointTip(long num)
    {
        mAddTipTxt.text = $"+{num}";
        mAddTipTxt.gameObject.SetActive(true);
        mAddTipTxt.transform.localScale = Vector3.zero;

        mAddTipTxt.DOFade(1, 0.2f);
        mAddTipTxt.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack).SetEase(Ease.OutBack).OnComplete(() =>
        {
            mAddTipTxt.DOFade(0, 0.2f).SetDelay(1.5f).OnComplete(() =>
            {
                mAddTipTxt.gameObject.SetActive(false);
            });
        });
    }

    private void FlyItems(long flyItemNum, GameObject item, Transform parent, Vector3 startPos, Vector3 endPos, Action first, Action complete)
    {
        flyItemNum = flyItemNum >= 10 ? 10 : flyItemNum;
        ;
        //贝塞尔曲线最大点数
        int bezierMaxPoint = 30;
        //贝塞尔曲线中点偏移量
        Vector3 bezierOffset = new Vector3();
        int count = 0;
        int instanceID = item.GetInstanceID();

        for (int i = 0; i < flyItemNum; i++)
        {
            Vector3[] posList = new Vector3[bezierMaxPoint];
            bezierOffset.x = UnityEngine.Random.Range(-200f, 200f);
            bezierOffset.y = (endPos.y - startPos.y) / 2;
            for (int b = 0; b < bezierMaxPoint; b++)
            {
                posList[b] = GetBezier2Pos(startPos, startPos + bezierOffset, endPos, b / (float) bezierMaxPoint);
            }

            Transform itemObj = null;
            if (mItemPool.ContainsKey(instanceID) && mItemPool[instanceID].Count > 0)
            {
                itemObj = mItemPool[instanceID][0];
                mItemPool[instanceID].RemoveAt(0);
            }
            else
            {
                itemObj = Instantiate(item, parent).transform;
            }

            itemObj.localScale = Vector3.one;
            itemObj.gameObject.SetActive(true);
            itemObj.localPosition = startPos;
            var sequence = DOTween.Sequence();
            sequence.AppendInterval(i * FlyItemDelayTime)
                .Append(itemObj.DOLocalPath(posList, FlyItemFlyTimeTotal, PathType.CatmullRom)).OnComplete(() =>
                {
                    if (!mItemPool.ContainsKey(instanceID))
                    {
                        mItemPool.Add(instanceID, new List<Transform>());
                    }

                    mItemPool[instanceID].Add(itemObj);
                    itemObj.gameObject.SetActive(false);
                    count++;
                    if (count >= flyItemNum)
                    {
                        complete?.Invoke();
                    }
                    else if (count == 1)
                    {
                        first?.Invoke();
                    }
                });
        }
    }
}
