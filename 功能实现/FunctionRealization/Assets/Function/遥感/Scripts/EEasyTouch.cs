using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EEasyTouch : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public RectTransform TouchParent;
    public RectTransform Touch;
    public List<GameObject> ArrowList = new List<GameObject>();
    
    //大小
    private float mSmallSize;

    private float mBigSize;

    //活动半径
    private float mRadius;

    //遥感初始位置
    private Vector2 mOrignPos;
    private CanvasGroup mGroup;


    

    public void OnBeginDrag(PointerEventData eventData)
    {
        //开始拖拽时显示
        mGroup.alpha = 1;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 v = Vector2.zero;
        //将屏幕坐标系转换成UI的
        RectTransformUtility.ScreenPointToLocalPointInRectangle(TouchParent, eventData.position, Camera.main, out v);
        Vector2 deltaPos = mOrignPos - v;

        float deltaDistance = Vector3.Distance(v, mOrignPos);
        //遥感在大圆内
        if (deltaDistance <= mRadius)
        {
            //圆内 遥感位置=鼠标位置
            Touch.transform.localPosition = v;
            UnActiveArrow();
        }
        //在圆外
        else
        {
            //圆外 遥感位置在大圆边上
            //delta的单位向量*半径+圆心初始位置得出在大圆边境上的位置
            Touch.transform.localPosition = -deltaPos.normalized * mRadius + mOrignPos;
            //遥感xy轴对应 stone的xz 形成一种映射 使stone旋转
            //求出弧度
            //为什么要用tan-----去看三角函数曲线，tan对应的是一个值，sincos可能对应多个值
            
            
            float tmpAngle = Mathf.Atan2(deltaPos.y, deltaPos.x);
            tmpAngle = Mathf.Rad2Deg * tmpAngle;
            Vector3 tmpEuler = Touch.localEulerAngles;
            tmpEuler.z = tmpAngle;
            
            bool leftButtom = tmpAngle <= 90 && tmpAngle > 0;
            bool rightButtom = tmpAngle <= 180 && tmpAngle > 90;
            bool leftTop = tmpAngle <= 0 && tmpAngle > -90;
            bool rightTop = tmpAngle <= -90 && tmpAngle > -180;
            ArrowList[0].SetActive(rightTop);
            ArrowList[1].SetActive(leftTop);
            ArrowList[2].SetActive(rightButtom);
            ArrowList[3].SetActive(leftButtom);
            
            //改变z值，使其旋转
            Touch.localEulerAngles = tmpEuler;
        }
    }

    private void UnActiveArrow()
    {
        foreach (var arrow in ArrowList)
        {
            arrow.SetActive(false);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //结束拖拽隐藏遥感
        mGroup.alpha = 0;
        //回归原位
        Touch.localPosition = mOrignPos;
        UnActiveArrow();
    }
    private void Start()
    {
        mGroup = Touch.GetComponent<CanvasGroup>();
        //没有点击时 把遥感隐藏
        mGroup.alpha = 0;

        mSmallSize = Touch.sizeDelta.x * 0.5f;
        mBigSize = TouchParent.sizeDelta.x * 0.5f;

        mRadius = mBigSize ;
        //记录初始位置
        mOrignPos = Touch.transform.localPosition;
    }
}
