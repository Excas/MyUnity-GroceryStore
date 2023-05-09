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

    private float m_RowHeight = 18f;
    private float m_ColWidth = 400;
    private Vector2 m_ScrollPosition;

    private Dictionary<FunctionFolder,string> mFuncFolder=new Dictionary<FunctionFolder, string>()
    {
        {FunctionFolder.Main,"/Function"},
        {FunctionFolder.Dotween,"/Function/Dotween动画"}
    };

    private string FindFunctionName;
    void OnGUI()
    {
        OnDrawFunctionList();
    }

    private  void OnDrawFunctionList()
    {
        GUILayout.BeginHorizontal();
        {
            FindFunctionName = EditorGUILayout.TextField("功能名:", FindFunctionName);
            if (GUILayout.Button("搜索", GUILayout.Width(50f)))
            {
                
            }
            if (GUILayout.Button("清除", GUILayout.Width(50f)))
            {
                
            }
        }
        GUILayout.EndHorizontal();
        CreateFunctionGroup(FunctionFolder.Main);
        CreateFunctionGroup(FunctionFolder.Dotween);
        m_ScrollPosition = EditorGUILayout.BeginScrollView(m_ScrollPosition);

      
        EditorGUILayout.EndScrollView();
    }

    private void CreateFunctionGroup(FunctionFolder functionFolder)
    {
        mFuncFolder.TryGetValue(functionFolder, out string targetFoler);
        string funcpath = Application.dataPath + targetFoler;
        List<string> files = GetNextFolder(funcpath);
        List<string> paths = GetNextFolderPath(funcpath);
        EditorGUILayout.LabelField(functionFolder.ToString());
        for (int i = 0; i < files.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button(files[i]))
            {
                OpenTargetScenes(paths[i]);
            }
            EditorGUILayout.EndHorizontal();
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
                floder.Add(subDir.Name); //输出子文件夹名称
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
                floder.Add(subDir.FullName); //输出子文件夹名称
            }
        }
        return floder;
    }

    private static void OpenTargetScenes(string targetPath)
    {
        DirectoryInfo dir = new DirectoryInfo(targetPath);
        bool hasScenes = false;
        if (dir.Exists)
        {
            foreach (FileInfo file in dir.GetFiles("*.unity", SearchOption.AllDirectories))
            {
                UnityEditor.SceneManagement.EditorSceneManager.OpenScene(file.FullName);
                Object obj = AssetDatabase.LoadAssetAtPath<Object>(@"Function\棋盘算法");
                Selection.activeObject = obj;
                EditorGUIUtility.PingObject(obj);
                hasScenes = true;
                break;
            }
        }
        if (!hasScenes)
        {
            Object obj = AssetDatabase.LoadAssetAtPath<Object>(@"Function\棋盘算法");
            Selection.activeObject = obj;
            EditorGUIUtility.PingObject(obj);
        }
        AssetDatabase.Refresh();
    }

    public enum FunctionFolder
    {
        Main,
        Dotween,
    }
}
