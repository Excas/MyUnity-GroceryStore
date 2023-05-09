using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KTweenAlpha : KTweener
{
    public float mFrom = 1;
    public float to = 1;

    public Graphic[] mGraphics;
    
    public List<string> mFilterNames;

    protected override void Awake()
    {
        if (mGraphics == null || mGraphics.Length == 0)
        {
            mGraphics = gameObject.GetComponentsInChildren<Graphic>();
        }
        base.Awake();
        OnFinished += ResetToEnd;
    }

    public override void Play()
    {
        ResetToBeginning();
        base.Play();
    }

    protected override void OnUpdate(float percent)
    {
        float alpha;
        if (IsInverse())
            alpha = Mathf.Lerp(to, mFrom, percent);
        else
            alpha = Mathf.Lerp(mFrom, to, percent);

        SetAlpha(alpha);
    }

    public virtual void SetAlpha(float alpha)
    {
        Color color;
        if (mGraphics != null)
        {
            for (int i = 0; i < mGraphics.Length; ++i)
            {
                if (mFilterNames != null && mFilterNames.Contains(mGraphics[i].name)) continue;
                color = mGraphics[i].color;
                color.a = alpha;
                mGraphics[i].color = color;
            }
        }
    }
    
    public override void ResetToBeginning()
    {
        SetAlpha(mFrom);
    }

    public override void ResetToEnd()
    {
        SetAlpha(to);
    }
}