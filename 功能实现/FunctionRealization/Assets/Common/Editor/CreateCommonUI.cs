using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
namespace Common.Editor
{
    public static class CreateCommonUI
    {
        [MenuItem("Tool/CommonUI/CreateCommonUI", false, 0)]
        public static void AddText()
        {
            GameObject.Instantiate(Resources.Load("UI/CommonUI"));
        }
    }
}
