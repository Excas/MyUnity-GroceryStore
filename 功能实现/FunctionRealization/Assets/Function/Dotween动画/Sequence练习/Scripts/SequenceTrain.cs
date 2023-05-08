using System.Collections.Generic;
using Common.CommonScripts;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SequenceTrain : FunctionBaseView
{
    public Image mBomb;
    public Image mPlayer;
    public Image mBox;
    public GameObject mReward;

    public Sprite mBomImg;
    private List<GameObject> mRwdList=new List<GameObject>();
    private void Start()
    {
        mBtn1.BtnTxt = "PlayAni";
        mBtn1.AddListener(SequenceAni);
    }

    private Sequence sequence;

    private void SequenceAni()
    {
        // * DOTween.Sequence()：创建一个新的Sequence对象。
        // * Append(Tween)：将Tween对象添加到序列的末尾，按照添加的顺序播放。
        // * Prepend(Tween)：将Tween对象添加到序列的开头，按照添加的顺序播放。
        // * Insert(float, Tween)：在指定的时间（以秒为单位）将Tween对象插入到序列中。
        // * Join(Tween)：将Tween对象与序列中的上一个Tween对象同时播放。
        // * JoinSequence(Sequence)：将另一个Sequence对象中的所有Tween对象添加到当前序列中，并同时播放。
        // * AppendInterval(float)：在序列中添加指定的延迟时间（以秒为单位）。
        // * PrependInterval(float)：在序列的开头添加指定的延迟时间（以秒为单位）。
        // * InsertInterval(float, float)：在指定的时间（以秒为单位）插入指定的延迟时间。
        // * AppendCallback(Action)：在序列的末尾添加一个回调函数，当Tween对象播放完成时调用。
        // * PrependCallback(Action)：在序列的开头添加一个回调函数，当Tween对象播放完成时调用。
        
        sequence.Kill();
        sequence = DOTween.Sequence()
            //飞到目标点
            .Append(mBomb.transform.DOLocalPath(Bessel.GetBezier(mBomb.transform.localPosition, mPlayer.transform.localPosition), 0.6f, PathType.CatmullRom))
            //炸弹效果
            .Append(mBomb.DOColor(Color.red, 0.2f).SetLoops(10, LoopType.Yoyo))
            .Join(mBomb.transform.DOScale(new Vector3(1.3f, 1.3f, 1f), 0.2f).SetLoops(10, LoopType.Yoyo))
            .AppendCallback(()=>{mBomb.sprite = mBomImg; })
            .Append(mBomb.DOColor(Color.red, 1))
            .Join(mBomb.transform.DOScale(Vector3.one * 3, 1))
            .AppendCallback(() => { mBomb.gameObject.SetActive(false); })
            //玩家效果
            .Append(mPlayer.DOColor(Color.red, 0.2f))
            .Append(mPlayer.DOFade(0, 0.2f))
            //激活奖励
            .AppendCallback(ActiveAllReward)
            .AppendInterval(0.2f);

        //奖励效果
        for (int i = 0; i < 5; i++)
        {
            GameObject o = Instantiate(mReward, mPlayer.transform);
            o.SetActive(false);
            Transform tran = o.transform;
            mRwdList.Add(o);
            sequence
                .Append(tran.DOLocalPath(Bessel.GetBezier(tran.localPosition, mBox.transform.localPosition), 0.2f))
                .AppendCallback(() => { o.SetActive(false); })
                .Append(mBox.transform.DOScale(Vector3.one*1.2f,0.03f).SetLoops(2,LoopType.Yoyo));//.SetDelay(disappearTime); 
        }
        //sequence.AppendCallback(CreateReward);
        sequence.OnComplete(() =>
        {
            Debug.Log("Ani Finish!!!");
        });
    }

    private void ActiveAllReward()
    {
        for (int i = 0; i < mRwdList.Count; i++)
        {
            mRwdList[i].SetActive(true);
        }
    }

    private void CreateReward()
    {
        for (int i = 0; i < 8; i++)
        {
            GameObject o = Instantiate(mReward, mPlayer.transform);
            Transform tran = o.transform;
            sequence.Append(tran.DOLocalPath(Bessel.GetBezier(tran.localPosition, mBox.transform.localPosition), 0.5f))
                .AppendCallback(() => { o.SetActive(false); })
                .Append(mBox.transform.DOScale(Vector3.one*1.2f,0.03f).SetLoops(2,LoopType.Yoyo));//.SetDelay(disappearTime); 
        }
    }
}
