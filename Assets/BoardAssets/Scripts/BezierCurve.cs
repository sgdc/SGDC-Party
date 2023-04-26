using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A static class used to get values from a bezier curve, as oppose to creating duplicate methods
/// </summary>
public class BezierCurve
{
    public Vector3 _startPoint;
    public Vector3 _endPoint;
    public Vector3 _bezierPoint1;
    public Vector3 _bezierPoint2;
    public BezierCurve(Vector3 _start,Vector3 _end,Vector3 _bezierPoint1,Vector3 _bezierPoint2)
    {
        this._startPoint = _start;
        this._endPoint = _end;
        this._bezierPoint1 = _bezierPoint1;
        this._bezierPoint2 = _bezierPoint2;
    }

    public float GetLength() { return GetLength(5); }
    public float GetLength(int stepCount)
    {
        return GetBezierLength(_startPoint, _endPoint, _bezierPoint1, _bezierPoint2, stepCount);
    }

    /// <summary>
    /// returns a position on the bezier curve
    /// </summary>
    /// <param name="t">the percantage of the curve to get the position from. Must be between 0 and 1 (inclusive)</param>
    /// <returns>returns a position on the bezier curve</returns>
    public Vector3 GetPosition(float t)
    {
        return PositionOnCurve(_startPoint, _endPoint, _bezierPoint1, _bezierPoint2, t);
    }


    /// <summary>
    /// returns a position on a bezier curve
    /// </summary>
    /// <param name="startPosition">the start point of the bezier curve</param>
    /// <param name="endPosition">the end point of the bezier curve</param>
    /// <param name="_bezierPoint">the offset of the bezier position from the start</param>
    /// <param name="t">the percantage of the curve to get the position from. Must be between 0 and 1 (inclusive)</param>
    /// <returns>Vector3 in world space of the position on the curve</returns>
    public static Vector3 PositionOnCurve(Vector3 startPosition,Vector3 endPosition,Vector3 _bezierPoint, float t)
    {
        //right now its only the quadratic implementation. Might add more overloads, so this should stay as its own class
        return Mathf.Pow(1 - t, 2) * startPosition + 2 * t * (1 - t) * _bezierPoint + Mathf.Pow(t, 2) * endPosition;
    }

    /// <summary>
    /// returns a position on a bezier curve
    /// </summary>
    /// <param name="startPosition">the start point of the bezier curve</param>
    /// <param name="endPosition">the end point of the bezier curve</param>
    /// <param name="_bezierPoint">the offset of the bezier position from the start</param>
    /// <param name="_bezierPoint2">the offset of the bezier position from the end</param>
    /// <param name="t">the percentage of the curve to get the position from. Must be between 0 and 1(inclusive)</param>
    /// <returns>Vector3 in world space of the position on the curve</returns>
    public static Vector3 PositionOnCurve(Vector3 startPosition, Vector3 endPosition, Vector3 _bezierPoint, Vector3 _bezierPoint2, float t)
    {
        return Mathf.Pow(1 - t, 3) * startPosition + 3 * Mathf.Pow(1 - t, 2) * _bezierPoint * t + 3 * (1 - t) * t * t * _bezierPoint2 + t * t * t * endPosition;
    }

    /// <summary>
    /// returns an approximate length of a bezier curve
    /// </summary>
    /// <param name="_startPosition">the start point of the bezier curve</param>
    /// <param name="_endPosition">the end point of the bezier curve</param>
    /// <param name="_bezierPoint">the offset of the bezier position from the start</param>
    /// <param name="_numberOfSteps">the number of line segments used to calculate the length of the curve. The more segments, the higher the accuracy, but the slower the performance. recommended 5-15</param>
    /// <returns>returns an approximate length of a bezier curve</returns>
    public static float GetBezierLength(Vector3 _startPosition, Vector3 _endPosition, Vector3 _bezierPoint, int _numberOfSteps)
    {
        float distance = 0f;
        float step = (1f / _numberOfSteps);
        for (float i = 0; i < 1f; i += step)
        {
            distance = Vector3.Distance(PositionOnCurve(_startPosition, _endPosition, _bezierPoint, i), PositionOnCurve(_startPosition, _endPosition, _bezierPoint, i+step));
        }
        return distance;
    }

    /// <summary>
    /// returns an approximate length of a bezier curve
    /// </summary>
    /// <param name="_startPosition">the start point of the bezier curve</param>
    /// <param name="_endPosition">the end point of the bezier curve</param>
    /// <param name="_bezierPoint">the offset of the bezier position from the start</param>
    /// <param name="_bezierPoint2">the offset of the bezier position from the end</param>
    /// <param name="_numberOfSteps">the number of line segments used to calculate the length of the curve. The more segments, the higher the accuracy, but the slower the performance. recommended 5-15</param>
    /// <returns>returns an approximate length of a bezier curve</returns>
    public static float GetBezierLength(Vector3 _startPosition, Vector3 _endPosition, Vector3 _bezierPoint,Vector3 _bezierPoint2, int _numberOfSteps)
    {
        float distance = 0f;
        float step = (1f / _numberOfSteps);
        for (float i = 0; i < 1f; i += step)
        {
            distance = Vector3.Distance(PositionOnCurve(_startPosition, _endPosition, _bezierPoint, _bezierPoint2, i), PositionOnCurve(_startPosition, _endPosition, _bezierPoint, _bezierPoint2, i + step));
        }
        return distance;
    }

    
    public static void DrawBezierInDebug(Vector3 _startPosition, Vector3 _endPosition, Vector3 _bezierPoint, int _numberOfSteps, Color _color)
    {
        float step = 1f / _numberOfSteps;
        for(float i = 0; i < 1f; i += step)
        {
            Vector3 _current = PositionOnCurve(_startPosition, _endPosition, _bezierPoint, i);
            Vector3 _next = PositionOnCurve(_startPosition, _endPosition, _bezierPoint, i+ step);
            Debug.DrawLine(_current, _next, _color);
        }
    }
    //overload
    public static void DrawBezierInDebug(Vector3 _startPosition, Vector3 _endPosition, Vector3 _bezierPoint, int _numberOfSteps) { DrawBezierInDebug(_startPosition, _endPosition, _bezierPoint,_numberOfSteps,Color.white); }


    /// <summary>
    /// draws a debug visualization of the bezier curve
    /// </summary>
    /// <param name="_startPosition">the start point of the bezier curve</param>
    /// <param name="_endPosition">the end point of the bezier curve</param>
    /// <param name="_bezierPoint">the offset of the bezier position from the start</param>
    /// <param name="_numberOfSteps"></param>
    /// <param name="_color"></param>
    public static void DrawBezierInDebug(Vector3 _startPosition, Vector3 _endPosition, Vector3 _bezierPoint, Vector3 _bezierPoint2,int _numberOfSteps, Color _color) {
        float step = 1f / _numberOfSteps;
        for (float i = 0; i < 1f; i += step)
        {
            Vector3 _current = PositionOnCurve(_startPosition, _endPosition, _bezierPoint,_bezierPoint2, i);
            Vector3 _next = PositionOnCurve(_startPosition, _endPosition, _bezierPoint,_bezierPoint2, i + step);
            Debug.DrawLine(_current, _next, _color);
        }
    }
}
