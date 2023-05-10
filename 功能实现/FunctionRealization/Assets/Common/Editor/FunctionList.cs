using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
public class FunctionList : EditorWindow
{
    [MenuItem("Tools/OpenFunctionView")]
    public static void Open()
    {
        var window = GetWindow<FunctionList>();
        window.minSize = new Vector2(400, 600);
    }
    
    private Vector2 mScrollPosition;
    private string mFinderName="";
    
    private Dictionary<FunctionFolder, FunctionFolderData> mFuncFolder = new Dictionary<FunctionFolder, FunctionFolderData>()
    {
        {FunctionFolder.Main, new FunctionFolderData{Path = "/Function"}},
        {FunctionFolder.Dotween, new FunctionFolderData{Path = "/Function/Dotween动画"}}
    };
    void OnGUI()
    {
        OnDrawFunctionList();
    }

    private void OnDrawFunctionList()
    {
        GUILayout.BeginHorizontal();
        {
            mFinderName = EditorGUILayout.TextField("功能名搜索:", mFinderName);
            if (GUILayout.Button("清除", GUILayout.Width(50f)))
            {
                mFinderName = "";
                CreateFunctionList();
                GUI.FocusControl("功能名搜索:");
            }
        }
        GUILayout.EndHorizontal();
        CreateFunctionList();
    }

    private void CreateFunctionList()
    {
        mScrollPosition = EditorGUILayout.BeginScrollView(mScrollPosition);
        CreateFunctionGroup(FunctionFolder.Main);
        CreateFunctionGroup(FunctionFolder.Dotween);
        EditorGUILayout.EndScrollView();
    }
    private bool isFold;
    private void CreateFunctionGroup(FunctionFolder functionFolder)
    {
        mFuncFolder.TryGetValue(functionFolder, out FunctionFolderData targetFoler);
        string funcpath = Application.dataPath + targetFoler.Path;
        List<string> files = GetNextFolder(funcpath);
        List<string> paths = GetNextFolderPath(funcpath);
        targetFoler.IsShow = EditorGUILayout.Foldout(targetFoler.IsShow,functionFolder.ToString());
        for (int i = 0; i < files.Count; i++)
        {

            if (targetFoler.IsShow)
            {
                EditorGUILayout.BeginHorizontal();
                if (files[i].Contains(mFinderName))
                {
                    if (GUILayout.Button(files[i]))
                    {
                        OpenTargetScenes(paths[i]);
                    }
                }
                EditorGUILayout.EndHorizontal();
            }
           
        }
    }
    private static List<string> GetNextFolder(string folderPath)
    {
        List<string> floder = new List<string>();
        DirectoryInfo dir = new DirectoryInfo(folderPath);
        if (dir.Exists)
        {
            foreach (DirectoryInfo subDir in dir.GetDirectories())
            {
                floder.Add(subDir.Name);
            }
        }
        return floder;
    }

    private static List<string> GetNextFolderPath(string folderPath)
    {
        List<string> floder = new List<string>();
        DirectoryInfo dir = new DirectoryInfo(folderPath);
        if (dir.Exists)
        {
            foreach (DirectoryInfo subDir in dir.GetDirectories())
            {
                floder.Add(subDir.FullName);
            }
        }
        return floder;
    }

    private static void OpenTargetScenes(string targetPath)
    {
        DirectoryInfo dir = new DirectoryInfo(targetPath);
        targetPath = targetPath.Substring(targetPath.IndexOf("Assets"));
        targetPath = targetPath.Replace('\\', '/');
        bool hasScenes = false;
        if (dir.Exists)
        {
            foreach (FileInfo file in dir.GetFiles("*.unity", SearchOption.AllDirectories))
            {
                hasScenes = true;
                UnityEditor.SceneManagement.EditorSceneManager.OpenScene(file.FullName);
                break;
            }
        }
        if (!hasScenes)
        {
            Debug.Log("沒有找到对应场景~~~");
        }
        Object obj = AssetDatabase.LoadAssetAtPath<Object>(targetPath);
        Selection.activeObject = obj;
        EditorGUIUtility.PingObject(obj);
        AssetDatabase.Refresh();
    }

    private enum FunctionFolder
    {
        Main,
        Dotween,
    }

    public class FunctionFolderData
    {
        public string Path;
        public bool IsShow=true;
    }
}
