using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class Bessel : MonoBehaviour
{
    public Button TriggerBtn;
    public Image Image_Icon1;
    public Slider slider;
    public TextMeshProUGUI addTipTxt;
    public GameObject Obj_itemForFly;
    public Transform viewRoot;

    //延迟移动时间、每块出发间隔时间
    private const float FlyItemDelayTime = 0.08f;
    //飞行总时间
    private const float FlyItemFlyTimeTotal = 0.6f;
    //半个对象池
    private static Dictionary<int, List<Transform>> ItemPools = new Dictionary<int, List<Transform>>();

    private float sliderValue;
    private void Awake()
    {
        TriggerBtn.onClick.AddListener(() => { SliderEffect(); });
    }

    private void SliderEffect()
    {
        long flyCount = UnityEngine.Random.Range(5, 12);
        Vector3 endPos = viewRoot.transform.InverseTransformPoint(Image_Icon1.transform.position);
        sliderValue += 0.2f;
        sliderValue = sliderValue > 1 ? 1 : sliderValue;
        FlyItems(flyCount,Obj_itemForFly, viewRoot, Vector3.zero, endPos, null, () =>
        {
            //飞完道具之后的操作
            ShowAddActPointTip(flyCount);
            DoHeadAni(flyCount);
            DoSliderAni(sliderValue);
        });
    }

    private void DoSliderAni(float time)
    {
        slider.DOValue(time, 1);
    }

    private void DoHeadAni(long shakeNum)
    {
        var sequence = DOTween.Sequence();
        sequence.Append(Image_Icon1.transform.DOScale(new Vector3(1.1f, 1.1f, 1f), 0.04f))
            .AppendCallback(PlayGoldFlyEndSound).SetLoops((int)shakeNum, LoopType.Yoyo);
    }

    private void PlayGoldFlyEndSound()
    {
        Debug.Log("播放飞行之后音效~");
    }

    private void ShowAddActPointTip(long num)
    {
        addTipTxt.text = $"+{num}";
        addTipTxt.gameObject.SetActive(true);
        addTipTxt.transform.localScale = Vector3.zero;

        addTipTxt.DOFade(1, 0.2f);
        addTipTxt.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack).SetEase(Ease.OutBack).OnComplete(() =>
        {
            addTipTxt.DOFade(0, 0.2f).SetDelay(1.5f).OnComplete(() => { addTipTxt.gameObject.SetActive(false); });
        });
    }

    private void FlyItems(long flyItemNum,GameObject item, Transform parent, Vector3 startPos, Vector3 endPos, Action first, Action complete)
    {
        //关键参数 资源总量、飞行块数、每块出发间隔时间、飞行总时间、贝塞尔曲线点数
        //资源总量(后续要通过它来计算飞行块总数)
        flyItemNum =  flyItemNum >= 10 ? 10 : flyItemNum;;
        //延迟移动时间、每块出发间隔时间
        float delayTime = FlyItemDelayTime;
        //飞行总时间
        float flyTimeTotal = FlyItemFlyTimeTotal;
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
            if (ItemPools.ContainsKey(instanceID) && ItemPools[instanceID].Count > 0)
            {
                itemObj = ItemPools[instanceID][0];
                ItemPools[instanceID].RemoveAt(0);
            }
            else
            {
                itemObj = Instantiate(item, parent).transform;
            }

            itemObj.localScale = Vector3.one;
            itemObj.gameObject.SetActive(true);
            itemObj.localPosition = startPos;
            var sequence = DOTween.Sequence();
            sequence.AppendInterval(i * delayTime)
                .Append(itemObj.DOLocalPath(posList, flyTimeTotal, PathType.CatmullRom)).OnComplete(() =>
                {
                    if (!ItemPools.ContainsKey(instanceID))
                    {
                        ItemPools.Add(instanceID, new List<Transform>());
                    }

                    ItemPools[instanceID].Add(itemObj);
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
    
    /// <summary>
    /// 获得贝塞尔曲线坐标
    /// </summary>
    private Vector3 GetBezier2Pos(Vector3 p0, Vector3 p1, Vector3 p2, float t)
    {
        float st = 1 - t;
        return st * st * p0 + 2 * t * st * p1 + t * t * p2;
    }
}
