using System.Collections.Generic;
using Common.CommonScripts;
using UnityEngine;

namespace Function.通用活动系统.Scripts.GameActivity
{
    public class GameActivityView: FunctionBaseView
    {
        public ActivityEntryGroup mEntryGroup;

        //UI
        public RectTransform mIconContent;
        public RectTransform mActivityViewRoot;

        public GameObject mActivityEntry;
        
        private List<IActivityEntry> _activityList=new List<IActivityEntry>();
        private void Start()
        {
            _activityList = GameActivityManager.Instance.GetActivityListByGroup(mEntryGroup);
            UpdateView();
        }

        private void UpdateView()
        {
            foreach (var act in _activityList)
            {
               GameObject actIcon= Instantiate(mActivityEntry, mIconContent, false);
               GameActivityEntry entry = actIcon.GetComponent<GameActivityEntry>();
               entry.SetData(act, mActivityViewRoot.gameObject);
            }
        }
    }
}