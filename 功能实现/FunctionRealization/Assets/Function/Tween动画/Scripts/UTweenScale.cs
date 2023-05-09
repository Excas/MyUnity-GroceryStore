using UnityEngine;

public class UTweenScale : UTweener
{
    public Vector3 mFrom;
    public Vector3 to;
    public Transform mTransform;

    public float extraDelay ;
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

    protected override void OnUpdate(float percent)
    {
        Vector3 scale;
        if (IsInverse())
            scale = Lerp(to, mFrom, percent);
        else
            scale = Lerp(mFrom, to, percent);
        SetScale(scale);
    }

    protected virtual void SetScale(Vector3 scale)
    {
        mTransform.localScale = scale;
    }

    public override void ResetToBeginning()
    {
        SetScale(mFrom);
    }

    public override void ResetToEnd()
    {
        SetScale(to);
    }
}

