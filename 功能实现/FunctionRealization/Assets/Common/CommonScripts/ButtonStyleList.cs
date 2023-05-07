using System;
using System.Collections.Generic;
using UnityEngine;
namespace Common.CommonScripts
{
    public enum EnumButtonStyle
    {
        None,
        Gray,
        Blue,
        Red,
        Green,
        Orange,
    }
    [CreateAssetMenu(menuName ="ScriptableObject/ButtonStyleList")]
    public class ButtonStyleList:ScriptableObject
    {
        public List<ButtonStyle> Styles=new List<ButtonStyle>();
        public ButtonStyle GetStyleByType(EnumButtonStyle style)
        {
            return Styles.Find(a => a.EnumButtonStyle == style);
        }
    }
    [Serializable]
    public class ButtonStyle
    {
        public Sprite ButtonBg;
        public Color Color;
        public EnumButtonStyle EnumButtonStyle;
    }
}
