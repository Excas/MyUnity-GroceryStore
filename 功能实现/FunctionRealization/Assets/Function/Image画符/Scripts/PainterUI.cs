using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PainterUI : MonoBehaviour
{

    private static Material lineMaterial;
    private List<Vector3> allPoints = new List<Vector3>();
    public Image mDrawTarget;
    //宽度
    public  int DrawLength = 3;
    static void CreateLineMaterial()
    {
        if (!lineMaterial)
        {
            // Unity has a built-in shader that is useful for drawing
            // simple colored things.
            Shader shader = Shader.Find("Hidden/Internal-Colored");
            lineMaterial = new Material(shader);
            lineMaterial.hideFlags = HideFlags.HideAndDontSave;
            // Turn on alpha blending
            lineMaterial.SetInt("_SrcBlend", (int) UnityEngine.Rendering.BlendMode.SrcAlpha);
            lineMaterial.SetInt("_DstBlend", (int) UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            // Turn backface culling off
            lineMaterial.SetInt("_Cull", (int) UnityEngine.Rendering.CullMode.Off);
            // Turn off depth writes
            lineMaterial.SetInt("_ZWrite", 0);
        }
    }
    
    // Will be called after all regular rendering is done
    public void OnRenderObject()
    {
        //给 画的线 穿上衣服 。
        CreateLineMaterial();
        // Apply the line material
        lineMaterial.SetPass(0);
        GL.PushMatrix();
        //将  物体坐标系 变换到世界坐标系。
        GL.MultMatrix(transform.localToWorldMatrix);
        //将Opengl 相机弄成 正交投影
        GL.LoadOrtho();
        
        GL.Begin(GL.LINES);
        
        GL.Color(Color.red);
        for (int i = 0; i < allPoints.Count - 1; i++)
        {
            GL.Vertex(allPoints[i]);

            GL.Vertex(allPoints[i + 1]);
        }
        
        GL.End();
        //矩阵用完了  出栈
        GL.PopMatrix();
    }
    void GenerateTexture()
    {
        //生成一个图片
        Texture2D tmpImage = new Texture2D((int)mDrawTarget.rectTransform.sizeDelta.x, (int)mDrawTarget.rectTransform.sizeDelta.y);

        for (int i = 0; i < allPoints.Count - 1; i++)
        {
            Vector2 frontPos = allPoints[i];

            Vector2 backPos = allPoints[i + 1];
           
            for (int j = 0; j < 100; j++)
            {
                int xx = (int) (Mathf.Lerp(frontPos.x, backPos.x, j / 100.0f) * tmpImage.width);
                int yy = (int) (Mathf.Lerp(frontPos.y, backPos.y, j / 100.0f) * tmpImage.height);
                int x, y;
                for (y = 0 - DrawLength; y < DrawLength; y++)
                {
                    for (x = 0 - DrawLength; x < DrawLength; x++)
                    {
                        tmpImage.SetPixel(xx - x, yy - y, Color.red);
                    }
                }
            }
        }

        tmpImage.Apply();
        Sprite sprite = Sprite.Create(tmpImage, new Rect(0, 0, tmpImage.width, tmpImage.height), new Vector2(0.5f, 0.5f));
        mDrawTarget.sprite= sprite;
    }
    
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 viewPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            allPoints.Add(viewPos);
        }

        if (Input.GetMouseButtonUp(0))
        {
            GenerateTexture();
            allPoints.Clear();
        }
    }
}
