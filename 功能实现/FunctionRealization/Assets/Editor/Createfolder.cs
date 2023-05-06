using System.IO;
using UnityEditor;
using UnityEngine;

public class Createfolder : EditorWindow
{
    [MenuItem("Tools/CreateFunctionFolder")]
    public static void AddWindow()
    {
        Rect wr = new Rect(0, 0, 400, 200);
        Createfolder window = (Createfolder) GetWindowWithRect(typeof(Createfolder), wr, true, "创建功能文件夹");
        window.Show();
    }
    
    private string functionName;
    private string path;
    private bool createPrefab = true;
    private bool createScript = true;
    private bool createScene = true;
    void OnGUI()
    {
        EditorGUILayout.Space();
        functionName = EditorGUILayout.TextField("功能名:", functionName);
        EditorGUILayout.Space();
        
        GUILayout.BeginHorizontal();
        {
            GUILayout.Label("文件夹生成路径", GUILayout.Width(100));
            path = GUILayout.TextField(path);
            if (GUILayout.Button("选择", GUILayout.Width(50f)))
            {
                path =EditorUtility.OpenFolderPanel("Select Folder", Application.dataPath+"/Function/", "");
            }
        }
        GUILayout.EndHorizontal();
        
        EditorGUILayout.Space();
        createPrefab = EditorGUILayout.Toggle("Prefabs", createPrefab);
        createScript = EditorGUILayout.Toggle("Scripts", createScript);
        createScene = EditorGUILayout.Toggle("Scene", createScene);
        EditorGUILayout.Space();
        if (GUILayout.Button("创建", GUILayout.Width(80), GUILayout.Height(40)) && !string.IsNullOrWhiteSpace(functionName))
        {
            GenerateFolder();
            ShowNotification(new GUIContent("创建成功"));
        }
    }


    private void GenerateFolder()
    {
        string prjPath = path+"/"+ functionName+"/";
        Directory.CreateDirectory(path+"/"+ functionName);
        if (createPrefab)
        {
            Directory.CreateDirectory(prjPath + "Prefabs");
        }
        
        if (createScript)
        {
            Directory.CreateDirectory(prjPath + "Scripts");
        }
        
        if (createScene)
        {
            Directory.CreateDirectory(prjPath + "Scenes");
        }
        
        AssetDatabase.Refresh();
    }
}
