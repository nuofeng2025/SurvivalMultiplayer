using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AngleUtils
{
    /// <summary>
    /// 归一化角度到 [0, 360)
    /// </summary>
    public static float NormalizeAngle(float angle)
    {
        angle %= 360f;
        if (angle < 0) angle += 360f;
        return angle;
    }

    /// <summary>
    /// 归一化角度到 [-180, 180)
    /// </summary>
    public static float NormalizeAngleSigned(float angle)
    {
        angle = NormalizeAngle(angle);
        if (angle > 180f) angle -= 360f;
        return angle;
    }

    /// <summary>
    /// 计算最短有符号角度差
    /// </summary>
    public static float DeltaAngle(float from, float to)
    {
        float delta = NormalizeAngle(to - from);
        if (delta > 180f) delta -= 360f;
        return delta;
    }

    /// <summary>
    /// 平滑角度（自动处理环绕）
    /// </summary>
    public static float SmoothDampAngle(float current, float target, ref float velocity, float smoothTime)
    {
        float delta = DeltaAngle(current, target);
        target = current + delta;
        return Mathf.SmoothDamp(current, target, ref velocity, smoothTime);
    }
}

