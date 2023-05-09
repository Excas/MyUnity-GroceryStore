using UnityEngine;

public abstract class AutoPlay : MonoBehaviour
{
    public enum PlayMoment
    {
        None,
        OnAwake,
        OnEnable,
        OnStart,
    }
    public PlayMoment mPlayMoment = PlayMoment.None;

    protected virtual void Awake()
    {
        if (mPlayMoment == PlayMoment.OnAwake)
        {
            Play();
        }
    }

    protected virtual void OnEnable()
    {
        if (mPlayMoment == PlayMoment.OnEnable)
        {
            Play();
        }
    }

    protected virtual void Start()
    {
        if (mPlayMoment == PlayMoment.OnStart)
        {
            Play();
        }
    }

    public virtual void Play()
    {
    }
}
