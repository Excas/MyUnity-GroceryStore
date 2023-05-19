using System;
using System.Collections.Generic;
using UnityEngine;

namespace Common.Editor.Function
{
    [CreateAssetMenu(menuName = "ScriptableObject/FunctionListData")]
    public class FunctionListData : ScriptableObject
    {
        public List<FunctionData> FunctionListPaths = new List<FunctionData>();

        public bool ContainsPath(string path)
        {
            foreach (var data in FunctionListPaths)
            {
                if (data.RelativePath == path)
                {
                    return true;
                }
            }

            return false;
        }
    }

    [Serializable]
    public class FunctionData
    {
        public string Name;

        /// <summary>
        /// 绝对路径
        /// </summary>
        public string AbsolutePath;

        /// <summary>
        /// 相对路径
        /// </summary>
        public string RelativePath => AbsolutePath.Substring(AbsolutePath.IndexOf("Assets")).Replace('\\', '/');

        public FunctionState State;
    }

    public enum FunctionState
    {
        NotStart,
        Dev,
        Finish,
    }
}