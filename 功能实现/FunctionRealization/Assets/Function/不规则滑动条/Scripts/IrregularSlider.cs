using System;
using System.Collections.Generic;

namespace Function.不规则滑动条.Scripts
{
    public class IrregularSlider
    {
        /// <summary>
        /// 计算不规则的进度条的进度
        /// </summary>
        /// <param name="cur">当前阶段值</param>
        /// <param name="stagesList">所有阶段值的list</param>
        /// <param name="cellWidth">单个阶段item的宽度</param>
        /// <returns>x轴比例</returns>
        public static float CalculateProgressValue(int cur, List<int> stagesList, float cellWidth)
        {
            if (cur <= 0)
            {
                return 0;
            }
            var list = new List<int>();
            list.Add(0);
            list.AddRange(stagesList);

            int curStageIdx = 0;
            float overflowPoint = 0;
            int stagePoint = 0;

            for (int i = 0; i < list.Count; i++)
            {
                int nextIdx = i + 1;
                if (nextIdx < list.Count)
                {
                    if (list[i] <= cur && list[nextIdx] > cur)
                    {
                        curStageIdx = i;
                        overflowPoint = cur - list[i];
                        stagePoint = list[nextIdx] - list[i];
                        break;
                    }
                }
                else
                {
                    curStageIdx = list.Count;
                    overflowPoint = cur - list[i];
                    stagePoint = list[i] - list[i - 1];
                }
            }

            float progressWidth = curStageIdx * cellWidth;
            float overflowWidth = stagePoint > 0 ? overflowPoint / stagePoint * cellWidth : 0.0f;

            float totalWidth = 1.0f;
            if (stagesList.Count > 0)
            {
                totalWidth = cellWidth * stagesList.Count;
            }

            float scale = (progressWidth + overflowWidth - 10) / totalWidth;
            scale = Math.Max(scale, 0.0f);
            scale = Math.Min(scale, 1.0f);
            return scale;
        }
    }
}