using UnityEngine;
using UnityEngine.UI;
namespace Common.CommonScripts
{
    public class FunctionBaseView : MonoBehaviour
    {
        protected CommonButton mBtn1;
        protected CommonButton mBtn2;
        protected CommonButton mBtn3;

        private void Awake()
        {
            mBtn1 = CreateButton(new Vector2(0,0));
            mBtn2 = CreateButton(new Vector2(0,-80));
            mBtn3 = CreateButton(new Vector2(0,-160));
            mBtn1.EnumButtonStyle = EnumButtonStyle.Red;
            mBtn2.EnumButtonStyle = EnumButtonStyle.Orange;
            mBtn3.EnumButtonStyle = EnumButtonStyle.Green;
            mBtn1.AddListener(OnBtn1Click);
            mBtn2.AddListener(OnBtn2Click);
            mBtn3.AddListener(OnBtn3Click);
        }

        private CommonButton CreateButton(Vector2 pos)
        {
            GameObject o = Instantiate(Resources.Load("UI/CommonButton")) as GameObject;
            o.transform.SetParent(transform);
            o.transform.localPosition=Vector3.zero;
            o.transform.localScale=Vector3.one;
            RectTransform rect = o.GetComponent<RectTransform>();
            rect.anchorMin= Vector3.one;
            rect.anchorMax = Vector3.one;
            rect.pivot = Vector3.one;
            rect.anchoredPosition  = pos;

            return o.GetComponent<CommonButton>();
        }

        protected virtual void OnBtn1Click()
        {
            
        }
        
        protected virtual void OnBtn2Click()
        {
            
        }
        
        protected virtual void OnBtn3Click()
        {
            
        }
    }
}
