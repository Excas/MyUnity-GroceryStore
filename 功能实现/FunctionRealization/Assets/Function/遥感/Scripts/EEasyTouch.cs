using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EEasyTouch : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public void OnBeginDrag(PointerEventData eventData)
    {
        //开始拖拽时显示
        group.alpha = 1;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 deltaPos = eventData.position - orignPos;

        float deltaDistance = Vector3.Distance(eventData.position, orignPos);
        //遥感在大圆内
        if (deltaDistance <= radius)
        {
            //圆内 遥感位置=鼠标位置
            transform.position = eventData.position;
        }
        //在圆外
        else
        {
            //圆外 遥感位置在大圆边上
            //delta的单位向量*半径+圆心初始位置得出在大圆边境上的位置
            transform.position = deltaPos.normalized * radius + orignPos;
        }
        //遥感xy轴对应 stone的xz 形成一种映射 使stone旋转
        //求出弧度
        //为什么要用tan-----去看三角函数曲线，tan对应的是一个值，sincos可能对应多个值
        float tmpAngle = Mathf.Atan2(deltaPos.y, deltaPos.x);

        tmpAngle = Mathf.Rad2Deg * tmpAngle;
        //Debug.Log("tmpAngle====" + tmpAngle);
        
        Vector3 tmpEuler = transform.localEulerAngles;

        tmpEuler.z = tmpAngle;
        //改变z值，使其旋转
        transform.localEulerAngles = tmpEuler;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //结束拖拽隐藏遥感
        group.alpha = 0;
        //回归原位
        transform.position = orignPos;
    }
    //大小
    float smallSize;
    float bigSize;
    //活动半径
    float radius;
    //遥感初始位置
    Vector2 orignPos;
    CanvasGroup group;
    private void Start()
    {
        group = GetComponent<CanvasGroup>();
        //没有点击时 把遥感隐藏
        group.alpha = 0;

        smallSize = ((RectTransform)transform).sizeDelta.x * 0.5f;
        bigSize = ((RectTransform)transform.parent).sizeDelta.x * 0.5f;
  
        radius = bigSize - smallSize;
       //记录初始位置
        orignPos =transform.position;
    }
   
}
