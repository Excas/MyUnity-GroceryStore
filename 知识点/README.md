# 知识点

##UI

* unity ui源码：https://github.com/Unity-Technologies/uGUI.git
* [UGUI源码一]6千字带你入门UGUI源码  https://zhuanlan.zhihu.com/p/437704772
* [UGUI源码分析：系统总结UGUI的全部内容]( https://blog.csdn.net/qq_28820675/article/details/106391292)

<details>
<summary>UI源码 阅后整理</summary>
1.ui消耗问题  绝大多数归根结底都是ui网格重建导致的UI的enable操作会导致该ui所有的脏标记 会进行重建  所以要避免ui组件设置enable <br />
2.Canvas下面每个ui发生变化 canvas就会重建（有特殊情况 改变image颜色） <br />
3.ui射线实现原理：GraphicRaycaster <br />
3.为什么 mask会产生两个drawcall   Mask组件会自动添加一个材质（材质跟其他组件无法合批）  mask实现-->模板缓存 需要遮罩的地方缓存值设为1 其他地方设置为0 <br />
4.RectMask2d 实现原理  通过计算移除区域外的顶点 区域外不会绘制顶点<br />
5. 两个RectMask2d下的子物体 即使材质相同也无法进行合批<br />
例如在Graphic 中存在三种脏标分别代表三种等待重建<br />
尺寸改变时（RectTransformDimensions）：LayoutRebuild 布局重建<br />
尺寸、颜色改变时：Vertices to GraphicRebuild 图像重建<br />
材质改变时：Material to GraphicRebuild 图像重建<br />
层级改变、应用动画属性（DidApplyAnimationProperties） ：All to Rebuild 重建所有<br />
</details>
