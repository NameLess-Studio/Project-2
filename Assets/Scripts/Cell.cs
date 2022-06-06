using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����� ��� ������ � ��������
/// </summary>
public class Cell : MonoBehaviour
{
    //�������� �� ���������:
    //1.7 �� ���������
    //2.8 �� �����������

    /// <summary>
    /// ���������� ������
    /// </summary>
    public Vector2 pos;
    /// <summary>
    /// ����, ������� �������� ������
    /// </summary>
    public Unit unit;

    #region ������ � ������������

    /// <summary>
    /// ������� ��������� ������ � ����������
    /// </summary>
    /// <param name="hex">���������� ������</param>
    /// <returns>���������� � ���������� �������</returns>
    private static Vector3 HexToCube(Vector2 hex)
    {
        Vector3 res = new Vector3();
        res.x = hex.x;
        res.z = hex.y;
        res.y = -res.x - res.z;
        return res;
    }
    /// <summary>
    /// ������� �� ��������� � ���������� �������
    /// </summary>
    /// <param name="cube">���������� � ���������� �������</param>
    /// <returns>���������� ������</returns>
    private static Vector2 CubeToHex(Vector3 cube)
    {
        return new Vector2(cube.x, cube.z);
    }

    /// <summary>
    /// ��������� ���������� �� �����
    /// </summary>
    /// <param name="h">���������� � ���������� �������</param>
    /// <returns>������������ ���������� � ���������� �������</returns>
    private static Vector2 Round(Vector3 h)
    {
        float rx = Mathf.Round(h.x);
        float ry = Mathf.Round(h.y);
        float rz = Mathf.Round(h.z);

        var x_diff = Mathf.Abs(rx - h.x);
        var y_diff = Mathf.Abs(ry - h.y);
        var z_diff = Mathf.Abs(rz - h.z);

        if (x_diff > y_diff && x_diff > z_diff)
            rx = -ry - rz;
        else if (y_diff > z_diff)
            ry = -rx - rz;
        else
            rz = -rx - ry;

        return CubeToHex(new Vector3(rx, ry, rz));
    }
    /// <summary>
    /// �������� ������������
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="t"></param>
    /// <returns>���������� � ���������� �������</returns>
    private static Vector3 CubeLerp(Vector3 a, Vector3 b, float t)
    {
        return new Vector3(Mathf.Lerp(a.x, b.x, t),
                           Mathf.Lerp(a.y, b.y, t),
                           Mathf.Lerp(a.z, b.z, t));
    }
    /// <summary>
    /// ��������� ��������� ����� ����� ��������
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns>���������� ����� a � b</returns>
    private static float HexDistance(Vector3 a, Vector3 b)
    {
        return (Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y) + Mathf.Abs(a.z - b.z)) / 2;
    }

    public static float Distance(Vector2 a, Vector2 b)
    {
        return HexDistance(HexToCube(a), HexToCube(b));
    }

    # endregion

    /// <summary>
    ///  ������������ ������ ���������� ����� ����� ��������
    /// </summary>
    /// <param name="start">������ ����������</param>
    /// <param name="finish">������ ����������</param>
    /// <param name="bf">���� ���</param>
    /// <returns>������ ������ ����������</returns>
    public static List<Cell> CreateTraectory(Cell start, Cell finish, Level bf)
    {
        Vector3 startPos = HexToCube(start.pos);
        Vector3 finishPos = HexToCube(finish.pos);
        float n = HexDistance(startPos, finishPos);
        List<Cell> traectory = new List<Cell>();
        for (int i = 0; i <= n; i++)
        {
            Vector2 pos = Round(CubeLerp(startPos, finishPos, 1.0f / n * i));
            traectory.Add(bf.FindCell(pos.x, pos.y));
        }
        return traectory;
    }

    public static List<Cell> ReconstructPath(Cell start, Cell finish, Level bf, Dictionary<Cell, Cell> cameFrom)
    {
        List<Cell> path = new List<Cell>();
        Cell current = finish;
        while (current != start)
        {
            path.Add(current);
            current = cameFrom[current];
        }
        path.Add(start);
        path.Reverse();
        return path;
    }
}
