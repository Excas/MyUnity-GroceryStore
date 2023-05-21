using UnityEditor;
using UnityEngine;
namespace Common.Editor.Function
{
    public class SetFunctionState
    {
        
        [MenuItem("Assets/设置功能状态/完成")]
        public static void FunctionFinish()
        {
            SetState(FunctionState.Finish);
        }
        
        [MenuItem("Assets/设置功能状态/进行中")]
        public static void FunctionDev()
        {
            SetState(FunctionState.Dev);
        }
        
        [MenuItem("Assets/设置功能状态/未开始")]
        public static void FunctionNotStart()
        {
            SetState(FunctionState.NotStart);
        }
        
        public static void SetState(FunctionState state)
        {
            FunctionListData listData = Resources.Load<FunctionListData>("ScriptableObject/FunctionList");
            //listData.ContainsPath();
            string[] guids = Selection.assetGUIDs;
            string assetPath = AssetDatabase.GUIDToAssetPath(guids[0]);
            if (listData.ContainsPath(assetPath,out var funcData))
            {
                funcData.State = state;
            }
            else
            {
                Debug.LogError("无法设置状态 功能路径不包含该路径：" + assetPath);
            }
            EditorUtility.SetDirty(listData);
            AssetDatabase.SaveAssets();
        }
    }
}
