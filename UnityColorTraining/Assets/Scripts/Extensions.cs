using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Extensions
{
    public static Color ToColor(this Phoenix.HSB96 hsb)
    {
        Phoenix.RGB96 rgb = hsb.ToRGB96();
        Color ret = new Color(rgb.r, rgb.g, rgb.b);
        return ret;
    }
    public static Vector2 PointPosInPercentage(this Rect rect, Vector3 point)
    {
        Vector2 ret = new Vector2();
        ret.x = (point.x - rect.x) / rect.width;
        ret.y = (point.y - rect.y) / rect.height;
        return ret;
    }
    public static Rect ToScreenRect(this RectTransform rectTransform)
    {
        Vector3[] corners = new Vector3[4];

        rectTransform.GetWorldCorners(corners);

        float xMin = float.PositiveInfinity;
        float xMax = float.NegativeInfinity;
        float yMin = float.PositiveInfinity;
        float yMax = float.NegativeInfinity;

        for (int i = 0; i < 4; i++)
        {
            // For Canvas mode Screen Space - Overlay there is no Camera; best solution I've found
            // is to use RectTransformUtility.WorldToScreenPoint) with a null camera.

            Vector3 screenCoord = RectTransformUtility.WorldToScreenPoint(null, corners[i]);

            if (screenCoord.x < xMin)
                xMin = screenCoord.x;
            if (screenCoord.x > xMax)
                xMax = screenCoord.x;
            if (screenCoord.y < yMin)
                yMin = screenCoord.y;
            if (screenCoord.y > yMax)
                yMax = screenCoord.y;
        }

        Rect result = new Rect(xMin, yMin, xMax - xMin, yMax - yMin);

        return result;
    }
    public static List<Rect> Splite(this Rect rect, int n)
    {
        List<Rect> ret = new List<Rect>();
        for (int i = 0; i < n; i++)
        {
            Rect r = new Rect();
            r.x = rect.x;
            r.y = rect.y + i * rect.height / n;
            r.width = rect.width;
            r.height = rect.height / n;
            ret.Add(r);
        }
        return ret;
    }
    public static void DrawSolidRect(this Texture2D texture,Rect rect,Phoenix.HSB96 color)
    {
        for (int y = (int)rect.y; y <rect.y+ rect.height; y++)
        {
            for (int x = (int)rect.x; x <rect.x+ rect.width; x++)
            {
                texture.SetPixel(x, y, color.ToColor());
            }
        }
    }
}
