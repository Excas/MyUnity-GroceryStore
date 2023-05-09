using UnityEngine;

public class KTweenPosition : KTweener
{
    public Vector3 mFrom;
    public Vector3 to;
    public Transform mTransform;
    public bool m_local;

    protected override void Awake()
    {
        if (mTransform == null)
        {
            mTransform = this.transform;
        }
        base.Awake();
        OnFinished += ResetToEnd;
    }

    public override void Play()
    {
        ResetToBeginning();
        base.Play();
    }

    protected override void OnPlayStart()
    {
        ResetToBeginning();
    }

    protected override void OnUpdate(float percent)
    {
        Vector3 pos;
        if (IsInverse())
            pos = Lerp(to, mFrom, percent);
        else
            pos = Lerp(mFrom, to, percent);

        ClampValue(ref pos);
        SetPosition(pos);
    }
    private void ClampValue(ref Vector3 pos)
    {
        float min = mFrom.x > to.x ? to.x : mFrom.x;
        float max = mFrom.x > to.x ? mFrom.x : to.x;
        pos.x = Mathf.Clamp(pos.x, min, max);
        min = mFrom.y > to.y ? to.y : mFrom.y;
        max = mFrom.y > to.y ? mFrom.y : to.y;
        pos.y = Mathf.Clamp(pos.y, min, max);
        min = mFrom.z > to.z ? to.z : mFrom.z;
        max = mFrom.z > to.z ? mFrom.z : to.z;
        pos.z = Mathf.Clamp(pos.z, min, max);
    }

    protected virtual void SetPosition(Vector3 pos)
    {
        if (m_local)
            mTransform.localPosition = pos;
        else
            mTransform.position = pos;
    }

    public override void ResetToBeginning()
    {
        if (!mReverse)
        {
            SetPosition(mFrom);
        }
        else
        {
            SetPosition(to);
        }
    }

    public override void ResetToEnd()
    {
        if (!mReverse)
        {
            SetPosition(to);
        }
        else
        {
            SetPosition(mFrom);
        }
    }

    public void ResetPosition(bool isFrom)
    {
        if (isFrom)
        {
            SetPosition(mFrom);
        }
        else
        {
            SetPosition(to);
        }

    }
}

