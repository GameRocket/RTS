using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionComponent : MonoBehaviour
{
    static Texture2D _whiteTexture;

    /// <summary>
    /// White Texture
    /// </summary>
    public static Texture2D WhiteTexture
    {
        get
        {
            if ( _whiteTexture == null )
            {
                _whiteTexture = new Texture2D(1, 1);
                _whiteTexture.SetPixel(0, 0, Color.white);
                _whiteTexture.Apply();
            }

            return _whiteTexture;
        }
    }

    /// <summary>
    /// Change graphical user interface color and draw white rectangle on it
    /// </summary>
    /// <param name="rect">Rectangle</param>
    /// <param name="color">Color</param>
    public static void DrawScreenRect(Rect rect, Color color)
    {
        GUI.color = color;
        GUI.DrawTexture(rect, WhiteTexture);
        GUI.color = Color.white;
    }

    /// <summary>
    /// In this method we can define our rectangle thickness and color
    /// </summary>
    /// <param name="rect">Rectangle</param>
    /// <param name="thickness">Thickness</param>
    /// <param name="color">Color</param>
    public static void DrawScreenRectBorder(Rect rect, float thickness, Color color)
    {
        //  Top
        Utils.DrawScreenRect(new Rect(rect.xMin, rect.yMin, rect.width, thickness), color);
        //  Left
        Utils.DrawScreenRect(new Rect(rect.xMin, rect.yMin, thickness, rect.height), color);
        //  Right
        Utils.DrawScreenRect(new Rect(rect.xMax - thickness, rect.yMin, thickness, rect.height), color);
        //  Bottom
        Utils.DrawScreenRect(new Rect(rect.xMin, rect.yMax - thickness, rect.width, thickness), color);
    }

    /// <summary>
    /// Get screen rectangle coordinates
    /// </summary>
    /// <param name="screenPosition1">Screen position one</param>
    /// <param name="screenPosition2">Screen position two</param>
    /// <returns>Returns four rectangle coordinate values</returns>
    public static Rect GetScreenRect(Vector3 screenPosition1, Vector3 screenPosition2)
    {
        //  Move origin from bottom left to top left
        screenPosition1.y = Screen.height - screenPosition1.y;
        screenPosition2.y = Screen.height - screenPosition2.y;

        //  Calculate corners
        var topLeft = Vector3.Min(screenPosition1, screenPosition2);
        var bottomRight = Vector3.Max(screenPosition1, screenPosition2);

        //  Create Rect
        return Rect.MinMaxRect(topLeft.x, topLeft.y, bottomRight.x, bottomRight.y);
    }
}
