
using UnityEngine;
using UnityEngine.UI;
public static class Uitils
{

    /// <summary>
    /// 检测当前鼠标是否在指定Ui内
    /// </summary>
    /// <param name="rect"></param>
    /// <returns></returns>
    public static bool IsPointerOverThisUI(RectTransform rect)
    {
        // 简单检测鼠标是否在当前UI上
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rect,
            Input.mousePosition,
            null,
            out localPoint
        );
        return rect.rect.Contains(localPoint);
    }







}
