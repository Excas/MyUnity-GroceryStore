using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Common.Editor.Function
{
    [CreateAssetMenu(menuName = "ScriptableObject/FunctionListData")]
    public class FunctionListData : ScriptableObject
    {
        public List<FunctionData> FunctionList = new List<FunctionData>();

        public bool ContainsPath(string path,out FunctionData func)
        {
            foreach (var data in FunctionList)
            {
                if (data.RelativePath == path || data.AbsolutePath == path) 
                {
                    func = data;
                    return true;
                }
            }
            func = null;
            return false;
        }

        public void AddFunction(FunctionData func)
        {
            if (!FunctionList.Exists(a=> a.Name==func.Name))
            {
                FunctionList.Add(func);
            }
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