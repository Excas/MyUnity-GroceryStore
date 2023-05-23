using System;
using System.Collections.Generic;
using UnityEngine;

namespace Function.通用活动系统.Scripts.Data
{
    [CreateAssetMenu(menuName = "ScriptableObject/GameActivityConfig")]
    public class GameActivityConfig : ScriptableObject
    {
        public List<ActivityConfig> ActivityConfigList=new List<ActivityConfig>();
    }

    [Serializable]
    public class ActivityConfig
    {
        public GameObject ActivityPrefab;
        public Sprite ActivityIcon;
        public int ActivityType;
        public string Name;
        public string Desc;
        public int Sort;
    }
}