using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
namespace Common.CommonScripts
{
    [Serializable]
    public class CommonButton : MonoBehaviour
    {
        public Button mBtn;
        public TextMeshProUGUI mTxt;
        public Image mBg;

        [SerializeField, SetProperty("EnumButtonStyle")]
        private EnumButtonStyle buttonStyle;
        public ButtonStyleList StyleList;
        public EnumButtonStyle EnumButtonStyle
        {
            get { return buttonStyle; }
            set
            {
                if (value != EnumButtonStyle.None)
                {
                    buttonStyle = value;
                    ResetButtonStyle();
                }
                else
                {
                    buttonStyle = EnumButtonStyle.None;
                }
            }
        }
        private void Start()
        {
            gameObject.SetActive(!string.IsNullOrEmpty(mTxt.text));
            ResetButtonStyle();
        }
        public void AddListener(UnityAction action)
        {
            mBtn.onClick.AddListener(action);
        }

        private void ResetButtonStyle()
        {
            ButtonStyle style = StyleList.GetStyleByType(buttonStyle);
            if (style!=null)
            {
                mBg.sprite = style.ButtonBg;
            }
        }
        public string BtnTxt
        {
            get
            {
                return mTxt.text;
            }
            set
            {
                mTxt.text = value;
                gameObject.SetActive(!string.IsNullOrEmpty(mTxt.text));
            }
        }
    }
}

