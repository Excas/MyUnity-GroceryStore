using UnityEngine;
using UnityEngine.UI;

public class HollowOutMask : MaskableGraphic, ICanvasRaycastFilter
{
    [SerializeField]
    private RectTransform _target;

    private Vector3 mTargetMin = Vector3.zero;
    private Vector3 nTargetMax = Vector3.zero;

    private bool mCanRefresh = true;
    private Transform mCacheTrans = null;
    private bool mIsRayPenetration = true;
    /// <summary>
    /// 设置镂空的目标
    /// </summary>
    public void SetTarget(RectTransform target)
    {
        mCanRefresh = true;
        _target = target;
        _RefreshView();
    }

    public void SetRayPenetration(bool b)
    {
        mIsRayPenetration = b;
    }

    private void _SetTarget(Vector3 tarMin, Vector3 tarMax)
    {
        if (tarMin == mTargetMin && tarMax == nTargetMax)
            return;
        mTargetMin = tarMin;
        nTargetMax = tarMax;
        SetAllDirty();
    }

    private void _RefreshView()
    {
        if (!mCanRefresh) return;
        mCanRefresh = false;

        if (null == _target)
        {
            _SetTarget(Vector3.zero, Vector3.zero);
            SetAllDirty();
        }
        else
        {
            Bounds bounds = RectTransformUtility.CalculateRelativeRectTransformBounds(mCacheTrans, _target);
            _SetTarget(bounds.min, bounds.max);
        }
    }

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        if (mTargetMin == Vector3.zero && nTargetMax == Vector3.zero)
        {
            base.OnPopulateMesh(vh);
            return;
        }
        vh.Clear();
        UIVertex vert = UIVertex.simpleVert;
        vert.color = color;

        Vector2 selfPiovt = rectTransform.pivot;
        Rect selfRect = rectTransform.rect;
        float outerLx = -selfPiovt.x * selfRect.width;
        float outerBy = -selfPiovt.y * selfRect.height;
        float outerRx = (1 - selfPiovt.x) * selfRect.width;
        float outerTy = (1 - selfPiovt.y) * selfRect.height;
        // 0 - Outer:LT
        vert.position = new Vector3(outerLx, outerTy);
        vh.AddVert(vert);
        // 1 - Outer:RT
        vert.position = new Vector3(outerRx, outerTy);
        vh.AddVert(vert);
        // 2 - Outer:RB
        vert.position = new Vector3(outerRx, outerBy);
        vh.AddVert(vert);
        // 3 - Outer:LB
        vert.position = new Vector3(outerLx, outerBy);
        vh.AddVert(vert);

        // 4 - Inner:LT
        vert.position = new Vector3(mTargetMin.x, nTargetMax.y);
        vh.AddVert(vert);
        // 5 - Inner:RT
        vert.position = new Vector3(nTargetMax.x, nTargetMax.y);
        vh.AddVert(vert);
        // 6 - Inner:RB
        vert.position = new Vector3(nTargetMax.x, mTargetMin.y);
        vh.AddVert(vert);
        // 7 - Inner:LB
        vert.position = new Vector3(mTargetMin.x, mTargetMin.y);
        vh.AddVert(vert);

        // 设定三角形
        vh.AddTriangle(4, 0, 1);
        vh.AddTriangle(4, 1, 5);
        vh.AddTriangle(5, 1, 2);
        vh.AddTriangle(5, 2, 6);
        vh.AddTriangle(6, 2, 3);
        vh.AddTriangle(6, 3, 7);
        vh.AddTriangle(7, 3, 0);
        vh.AddTriangle(7, 0, 4);
    }

    bool ICanvasRaycastFilter.IsRaycastLocationValid(Vector2 screenPos, Camera eventCamera)
    {
        if (!mIsRayPenetration) return true;
        if (null == _target) return true;
        // 将目标对象范围内的事件镂空（使其穿过）
        return !RectTransformUtility.RectangleContainsScreenPoint(_target, screenPos, eventCamera);
    }

    protected override void Awake()
    {
        base.Awake();
        mCacheTrans = GetComponent<RectTransform>();
    }

#if UNITY_EDITOR
    void Update()
    {
        mCanRefresh = true;
        _RefreshView();
    }
#endif
}