using UnityEngine;

public class AutoRun : AutoPlay
{
    public enum Style
    {
        Once,
        Loop,
        PingPong,
    }
    public Style mStyle;
    public bool mReverse;
    public float mDelay;
    public float mDuration = 1;

    protected float mTime;
    protected bool mPlaying;
    protected bool mRunning;
    protected bool mBackward;
    
    public override void Play()
    {
        mTime = 0;
        mPlaying = false;
        mRunning = true;
        mBackward = false;
        this.enabled = true;

        if (mDelay < 1E-5f)
        {
            PlayStart();
        }
    }

    public virtual void Stop()
    {
        mPlaying = false;
        mRunning = false;
        if (mPlayMoment != PlayMoment.OnEnable)
            this.enabled = false;
    }

    public virtual void Pause()
    {
        mRunning = false;
    }

    public virtual void Resume()
    {
        mRunning = true;
    }

    public void SetReverse(bool reverse)
    {
        mReverse = reverse;
    }

    private void PlayStart()
    {
        mPlaying = true;
        OnPlayStart();
    }

    protected virtual void PlayEnd()
    {
        OnPlayEnd();
        Stop();
    }

    protected virtual void OnPlayStart()
    {
    }

    protected virtual void OnPlayEnd()
    {
    }

    protected virtual void OnUpdate()
    {
    }

    protected bool IsInverse()
    {
        return mReverse ^ mBackward;
    }

    void Update()
    {
        if (mRunning)
        {
            mTime += Time.deltaTime;

            if (!mPlaying)
            {
                if (mTime >= mDelay)
                {
                    PlayStart();
                }
            }

            OnUpdate();

            if (mTime >= mDelay + mDuration)
            {
                switch (mStyle)
                {
                    case Style.Loop:
                        {
                            mTime = 0;
                        }
                        break;
                    case Style.PingPong:
                        {
                            mTime = 0;
                            mBackward = !mBackward;
                        }
                        break;
                    default:
                        {
                            PlayEnd();
                        }
                        break;
                }
            }
        }
    }
}

