using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityDebug : MonoBehaviour
{

    public static void DrawCircle(Vector3 center, Vector3 up, Color color, float radius)
    {


        up = (up == Vector3.zero ? Vector3.up : up).normalized * radius;
        var forward = Vector3.Slerp(up, -up, 0.5f);
        var right = Vector3.Cross(up, forward).normalized * radius;
        for (var i = 1; i < 26; i++)
        {
            Debug.DrawLine(center + Vector3.Slerp(forward, right, (i - 1) / 25f), center + Vector3.Slerp(forward, right, i / 25f), color);
            Debug.DrawLine(center + Vector3.Slerp(forward, -right, (i - 1) / 25f), center + Vector3.Slerp(forward, -right, i / 25f), color);
            Debug.DrawLine(center + Vector3.Slerp(right, -forward, (i - 1) / 25f), center + Vector3.Slerp(right, -forward, i / 25f), color);
            Debug.DrawLine(center + Vector3.Slerp(-right, -forward, (i - 1) / 25f), center + Vector3.Slerp(-right, -forward, i / 25f), color);
        }


    }

    public static void DrawCapsule(Vector3 pos, Vector3 center, Vector3 scale, CapsuleDirection direction, float radius, float height, Color color)
    {
        if (height < 0f)
        {
            return;
        }
        if (radius < 0f)
        {
            return;
        }
        // 根据朝向找到up 和 高度缩放值
        Vector3 up = Vector3.up;
        // 半径缩放值
        float radiusScale = 1f;
        // 高度缩放值
        float heightScale = 1f;
        switch (direction)
        {
            case CapsuleDirection.XAxis:
                up = Vector3.right;
                heightScale = Mathf.Abs(scale.x);
                radiusScale = Mathf.Max(Mathf.Abs(scale.y), Mathf.Abs(scale.z));
                break;
            case CapsuleDirection.YAxis:
                up = Vector3.up;
                heightScale = Mathf.Abs(scale.y);
                radiusScale = Mathf.Max(Mathf.Abs(scale.x), Mathf.Abs(scale.z));
                break;
            case CapsuleDirection.ZAxis:
                up = Vector3.forward;
                heightScale = Mathf.Abs(scale.z);
                radiusScale = Mathf.Max(Mathf.Abs(scale.x), Mathf.Abs(scale.y));
                break;
        }

        float realRadius = radiusScale * radius;
        height = height * heightScale;
        float sideHeight = Mathf.Max(height - 2 * realRadius, 0f);

        center = new Vector3(center.x * scale.x, center.y * scale.y, center.z * scale.z);
        // 为了符合Unity的CapsuleCollider的绘制样式，调整位置
        pos = pos - up.normalized * (sideHeight * 0.5f + realRadius) + center;


        up = up.normalized * realRadius;
        Vector3 forward = Vector3.Slerp(up, -up, 0.5f);
        Vector3 right = Vector3.Cross(up, forward).normalized * realRadius;

        Vector3 start = pos + up;
        Vector3 end = pos + up.normalized * (sideHeight + realRadius);

        // 半径圆
        DrawCircle(start, up, color, realRadius);
        DrawCircle(end, up, color, realRadius);

        // 边线
        Debug.DrawLine(start - forward, end - forward, color);
        Debug.DrawLine(start + right, end + right, color);
        Debug.DrawLine(start - right, end - right, color);
        Debug.DrawLine(start + forward, end + forward, color);
        Debug.DrawLine(start - forward, end - forward, color);

        for (int i = 1; i < 26; i++)
        {
            // 下部的头
            Debug.DrawLine(start + Vector3.Slerp(right, -up, (i - 1) / 25f), start + Vector3.Slerp(right, -up, i / 25f), color);
            Debug.DrawLine(start + Vector3.Slerp(-right, -up, (i - 1) / 25f), start + Vector3.Slerp(-right, -up, i / 25f), color);
            Debug.DrawLine(start + Vector3.Slerp(forward, -up, (i - 1) / 25f), start + Vector3.Slerp(forward, -up, i / 25f), color);
            Debug.DrawLine(start + Vector3.Slerp(-forward, -up, (i - 1) / 25f), start + Vector3.Slerp(-forward, -up, i / 25f), color);

            // 上部的头
            Debug.DrawLine(end + Vector3.Slerp(forward, up, (i - 1) / 25f), end + Vector3.Slerp(forward, up, i / 25f), color);
            Debug.DrawLine(end + Vector3.Slerp(-forward, up, (i - 1) / 25f), end + Vector3.Slerp(-forward, up, i / 25f), color);
            Debug.DrawLine(end + Vector3.Slerp(right, up, (i - 1) / 25f), end + Vector3.Slerp(right, up, i / 25f), color);
            Debug.DrawLine(end + Vector3.Slerp(-right, up, (i - 1) / 25f), end + Vector3.Slerp(-right, up, i / 25f), color);
        }


    }


}

[System.Serializable]
public enum CapsuleDirection
{
    XAxis,
    YAxis,
    ZAxis
}


[System.Serializable]
public struct Capsule
{
    public Vector3 pos;
    public Vector3 center;
    public Vector3 scale;
    public CapsuleDirection direction;
    public float radius;
    public float height;
    public Color color;
}