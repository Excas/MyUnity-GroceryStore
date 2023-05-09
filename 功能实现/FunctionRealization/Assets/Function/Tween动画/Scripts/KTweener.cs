using System;
using UnityEngine;

    public class KTweener : TweenRun
    {
        public AnimationCurve mCurve;
        protected Action OnFinished;

        private int mCurveKeyLenght = -1;

        public override void Play()
        {
            Play(false);
        }

        public virtual void Play(bool reset)
        {
            if (reset)
            {
                ResetToBeginning();
            }

            base.Play();
        }


        public virtual void PlayForward()
        {
            SetReverse(false);
            SetBackward(false);
            Play();
        }

        public virtual void PlayBackward()
        {
            SetReverse(true);
            SetBackward(true);
            Play();
        }

        protected override void OnUpdate()
        {
            if (mCurveKeyLenght == -1)
            {
                mCurveKeyLenght = mCurve == null ? 0 : mCurve.keys.Length;
            }

            if (mTime >= mDelay)
            {
                float percent = (mTime - mDelay) / mDuration;
                if (mCurve != null && mCurveKeyLenght > 0)
                {
                    percent = mCurve.Evaluate(percent);
                }

                OnUpdate(percent);
            }
        }

        protected virtual void OnUpdate(float percent)
        {
        }

        protected override void OnPlayEnd()
        {
            OnFinished?.Invoke();
        }

        public virtual void ResetToBeginning()
        {
        }

        public virtual void ResetToEnd()
        {
        }

        public static Vector3 Lerp(Vector3 mFrom, Vector3 to, float t)
        {
            Vector3 d = to - mFrom;
            return mFrom + d.normalized * (d.magnitude * t);
        }

        public static KTweener MaxTweener(KTweener[] tweeners)
        {
            KTweener max = null;
            for (int i = 0; i < tweeners.Length; ++i)
            {
                var t = tweeners[i];
                if (max == null || max.mDelay + max.mDuration < t.mDelay + t.mDuration)
                {
                    max = t;
                }
            }

            return max;
        }

        public static void PlayTween(KTweener[] tweeners, bool resetToBeginning)
        {
            for (int i = 0; i < tweeners.Length; ++i)
            {
                tweeners[i].Play(resetToBeginning);
            }
        }

        public static void StopTween(KTweener[] tweeners, bool resetToEnd)
        {
            for (int i = 0; i < tweeners.Length; ++i)
            {
                tweeners[i].Stop();
                tweeners[i].ResetToEnd();
            }
        }

        public float GetTime()
        {
            return mTime;
        }

        public void SetTime(float time)
        {
            mTime = time;
        }

        public bool GetBackward()
        {
            return mBackward;
        }

        public void SetBackward(bool back)
        {
            mBackward = back;
        }
    }