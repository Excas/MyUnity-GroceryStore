using System;
using UnityEngine;
using UnityEngine.UI;

namespace Function.通用活动系统.Scripts.GameActivity
{
    public class GameActivityEntry : MonoBehaviour
    {
        public Button mIconBtn;
        private IActivityEntry mEntry;
        private GameObject mActRoot;
        private void Start()
        {
            mIconBtn.onClick.AddListener(OnIconClick);
        }

        private void OnIconClick()
        {
            mEntry.Activity.ShowActivityView(mActRoot);
        }

        public void SetData(IActivityEntry activityEntry,GameObject root)
        {
            mEntry = activityEntry;
            mActRoot = root;
            transform.GetComponent<Image>().sprite = mEntry.Activity.GetActInfo().ActivityIcon;
        }
    }
}