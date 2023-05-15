// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.EventSystems;
// using UnityEngine.UI;
// /// <summary>
// /// UI管理器
// /// </summary>
// public class UIManager : SingletonPatternMonoAutoBase_DontDestroyOnLoad<UIManager>
// {
//     //最后方的层的Transform
//     private Transform rearmost;
//     public Transform Rearmost
//     {
//         get
//         {
//             if (rearmost == null)
//                 rearmost = transform.Find("Rearmost");
//             return rearmost;
//         }
//     }
//
//     //后方的层
//     private Transform rear;
//     public Transform Rear
//     {
//         get
//         {
//             if (rear == null)
//                 rear = transform.Find("Rear");
//             return rear;
//         }
//     }
//
//     //中间的层
//     private Transform middle;
//     public Transform Middle
//     {
//         get
//         {
//             if (middle == null)
//                 middle = transform.Find("Middle");
//             return middle;
//         }
//     }
//
//     //前面的层
//     private Transform front;
//     public Transform Front
//     {
//         get
//         {
//             if (front == null)
//                 front = transform.Find("Front");
//             return front;
//         }
//     }
//
//     //最前面的层
//     private Transform forefront;
//     public Transform Forefront
//     {
//         get
//         {
//             if (forefront == null)
//                 forefront = transform.Find("Forefront");
//             return forefront;
//         }
//     }
//
//     //记录当前已经显示的面板
//     Dictionary<GameObject, GameObject> panelDictionary = new Dictionary<GameObject, GameObject>();
//
//     /// <summary>
//     /// 显示面板
//     /// </summary>
//     public void ShowPanel(GameObject panelPrefab,E_PanelDisplayedLayer layer=E_PanelDisplayedLayer.Middle)
//     {
//         //确保面板的预制体不为null
//         if (panelPrefab == null)
//         {
//             Debug.LogWarning("显示面板失败！要显示的面板的预制体为null，请确保该预制体加载成功！");
//             return;
//         }
//
//         //如果当前已经显示了该面板，就不会重复显示了，而是直接返回。
//         if (panelDictionary.ContainsKey(panelPrefab))
//         {
//             return;
//         }
//
//         //创建指定的面板物体
//         GameObject panel = Instantiate(panelPrefab);
//
//         //给面板改名
//         panel.name = panelPrefab.name;
//
//         //将该面板的信息记录到字典中
//         panelDictionary.Add(panelPrefab, panel);
//
//         //记录该面板要放进哪个层中来显示
//         Transform parent = null;
//
//         switch (layer)
//         {
//             case E_PanelDisplayedLayer.Rearmost:
//                 parent = Rearmost;
//                 break;
//             case E_PanelDisplayedLayer.Rear:
//                 parent = Rear;
//                 break;
//             case E_PanelDisplayedLayer.Middle:
//                 parent = Middle;
//                 break;
//             case E_PanelDisplayedLayer.Front:
//                 parent = Front;
//                 break;
//             case E_PanelDisplayedLayer.Forefront:
//                 parent = Forefront;
//                 break;
//             default:
//                 break;
//         }
//
//         //修正面板的位置
//         panel.transform.SetParent(transform, false);
//
//         //在指定的层显示该面板
//         panel.transform.SetParent(parent);
//
//
//     }
//
//     /// <summary>
//     /// 隐藏面板
//     /// </summary>
//     public void HidePanel(GameObject panelPrefab)
//     {
//         //如果要隐藏的面板为null，则隐藏无效。
//         if (panelPrefab==null)
//         {
//             return;
//         }
//
//         //如果要隐藏的面板不存在，直接返回。
//         if (!panelDictionary.ContainsKey(panelPrefab))
//         {
//             return;
//         }
//
//         //销毁面板
//         Destroy(panelDictionary[panelPrefab]);
//
//         //移除字典中该面板的信息
//         panelDictionary.Remove(panelPrefab);
//     }
//
//     /// <summary>
//     /// 隐藏面板
//     /// </summary>
//     public void HidePanel(string panelPrefabName)
//     {
//         foreach (var item in panelDictionary.Keys)
//         {
//             if (item.name == panelPrefabName)
//             {
//                 HidePanel(item);
//                 return;
//             }
//         }
//     }
//
//     /// <summary>
//     /// 判断指定的面板是否显示了
//     /// </summary>
//     public bool IsPanelShowed(GameObject panelPrefab)
//     {
//         if (panelPrefab == null)
//         {
//             return false ;
//         }
//
//         return panelDictionary.ContainsKey(panelPrefab);
//     }
//
//     /// <summary>
//     /// 判断指定的面板是否显示了
//     /// </summary>
//     public bool IsPanelShowed(string panelPrefabName)
//     {
//         foreach (var item in panelDictionary.Keys)
//         {
//             if (item.name == panelPrefabName)
//                 return true;
//         }
//
//         return false;
//     }
//
//
//
//
//
//
//
//     void Awake()
//     {
//         //创建画布
//         CreateOverlayCanvas();
//
//
//
//         //创建EventSystem
//         CreateEventSystem();
//
//
//     }
//     void CreateOverlayCanvas()
//     {
//         //改Layer
//         gameObject.layer = LayerMask.NameToLayer("UI");
//
//         //添加并设置Canvas组件
//         Canvas canvas = gameObject.AddComponent<Canvas>();
//         canvas.renderMode = RenderMode.ScreenSpaceOverlay;
//         canvas.sortingOrder = 30000;
//
//         //添加并设置CanvasScaler组件
//         CanvasScaler canvasScaler = gameObject.AddComponent<CanvasScaler>();
//         canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
//         canvasScaler.referenceResolution = new Vector2(Screen.width, Screen.height);
//         canvasScaler.matchWidthOrHeight = Screen.width > Screen.height ? 1 : 0;
//
//         //添加并设置Graphic Raycaster组件
//         GraphicRaycaster graphicRaycaster = gameObject.AddComponent<GraphicRaycaster>();
//
//         //添加子物体，作为显示的层的父物体
//         //Rearmost层的父物体
//         GameObject rearmost = new GameObject("Rearmost");
//         rearmost.transform.SetParent(transform, false);
//
//         //Rear层的父物体
//         GameObject rear = new GameObject("Rear");
//         rear.transform.SetParent(transform, false);
//
//         //Middle层的父物体
//         GameObject middle = new GameObject("Middle");
//         middle.transform.SetParent(transform, false);
//
//         //Fornt层的父物体
//         GameObject front = new GameObject("Front");
//         front.transform.SetParent(transform, false);
//
//         //Frontmost层的父物体
//         GameObject foreFront = new GameObject("Forefront");
//         foreFront.transform.SetParent(transform, false);
//     }
//
//     void CreateEventSystem()
//     {
//         //如果场景中已经有一个EventSystem了，则直接返回。
//         if (FindObjectOfType<EventSystem>()) return;
//
//         GameObject eventSystem = new GameObject("EventSystem");
//         DontDestroyOnLoad(eventSystem);//切换场景不销毁
//         eventSystem.AddComponent<EventSystem>();
//         eventSystem.AddComponent<StandaloneInputModule>();
//     }
//
//
//
//
//
//
//
//
//
//
//
// }
