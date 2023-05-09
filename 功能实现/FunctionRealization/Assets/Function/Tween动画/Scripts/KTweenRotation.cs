using UnityEngine;

public class KTweenRotation : KTweener
{
    public Vector3 mFrom;
    public Vector3 to;
    public Transform mTransform;
    public bool mLocal;

    protected override void Awake()
    {
        if (mTransform == null)
        {
            mTransform = transform;
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
        Vector3 pos;
        if (IsInverse())
            pos = Lerp(to, mFrom, percent);
        else
            pos = Lerp(mFrom, to, percent);
        SetRotation(pos);
    }

    protected virtual void SetRotation(Vector3 euler)
    {
        if (mLocal)
            mTransform.localRotation = Quaternion.Euler(euler);
        else
            mTransform.rotation = Quaternion.Euler(euler);
    }

    public override void ResetToBeginning()
    {
        SetRotation(mFrom);
    }

    protected override void OnPlayEnd()
    {
        base.OnPlayEnd();
        this.ResetToEnd();
    }
    public override void ResetToEnd()
    {
        SetRotation(to);
    }
}