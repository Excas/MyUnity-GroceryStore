using System.Collections.Generic;
using Common.Editor.Function;
using UnityEditor;
using UnityEngine;

namespace CatAsset.Editor
{
    /// <summary>
    /// 绘制Project窗口下，文件/文件夹描述信息的工具类
    /// </summary>
    public static class DrawDescTool
    {
        private static Dictionary<FunctionState,Color> mFuncColor=new Dictionary<FunctionState, Color>()
        {
            {FunctionState.Finish,Color.gray},
            {FunctionState.Dev,Color.blue},
            {FunctionState.NotStart,Color.black}
        };
        
        [InitializeOnLoadMethod]
        private static void InitializeOnLoadMethod()
        {
            EditorApplication.projectWindowItemOnGUI += OnGUI;
        }

        private static void OnGUI(string guid, Rect selectionRect)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            
            FunctionListData listData = Resources.Load<FunctionListData>("ScriptableObject/FunctionList");
            if (AssetDatabase.IsValidFolder(path)&& listData.ContainsPath(path,out FunctionData data))
            {
                mFuncColor.TryGetValue(data.State, out var stateColor);
                DrawDesc(data.State.ToString(),selectionRect,stateColor);
            }
        }

        /// <summary>
        /// 绘制Project面板下文件/文件夹后的描述信息
        /// </summary>
        private static void DrawDesc(string desc,Rect selectionRect,Color descColor)
        {

            if (selectionRect.height > 16)
            {
                //图标视图
                return;
            }

            GUIStyle label = EditorStyles.label;
            GUIContent content = new GUIContent(desc);

            Rect pos = selectionRect;

            //只在列表视图绘制
            float width = label.CalcSize(content).x + 10;
            pos.x = pos.xMax - width;  //绘制在最右边
            pos.width = width;
            pos.yMin++;

            Color color = GUI.color;
            GUI.color = descColor;
            GUI.DrawTexture(pos, EditorGUIUtility.whiteTexture);
            GUI.color = color;
            GUI.Label(pos, desc);


        }
    }
}
