using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    private static readonly Vector2[] DIRS = new[]
        {
            new Vector2(1, 0),
            new Vector2(0, -1),
            new Vector2(-1, 0),
            new Vector2(0, 1),
            new Vector2(1, -1),
            new Vector2(-1, 1)
        };
    /// <summary>
    /// Список всех клеток поля
    /// </summary>
    [SerializeField]
    private List<Cell> cells = new List<Cell>();
    /// <summary>
    /// Список всех врагов на поле
    /// </summary>
    public List<Enemy> enemies = new List<Enemy>();
    public Ally hero;

    [Header("Генератор клеток")]
    public GameObject cellPrefab;

    // Отступы по осям
    public Vector2 offsetX;
    public Vector2 offsetY;

    // Координаты прямоугольника
    public Vector2Int start;
    public Vector2Int end;

    private void Awake()
    {
        Cell[] c = GetComponentsInChildren<Cell>();
        cells = new List<Cell>(c);
    }

    public void CreateCells()
    {
        for (int x = start.x; x < end.x; x++)
            for (int y = start.y; y < end.y; y++)
            {
                Vector3 pos = Vector3.zero;
                pos.x += offsetX.x * x + offsetY.x * y;
                pos.z += offsetX.y * x + offsetY.y * y;
                Cell cell = Instantiate(cellPrefab, pos, cellPrefab.transform.rotation, transform).GetComponent<Cell>();
                cell.pos = new Vector2(x, y);
            }
    }

    /// <summary>
    /// Поиск клеток по их координатам
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public Cell FindCell(float x, float y)
    {
        return cells.Find(cell => cell.pos.x == x && cell.pos.y == y);
    }

    public Cell FindCell(Vector2 pos)
    {
        return cells.Find(cell => cell.pos == pos);
    }

    public List<Cell> Neighbors(Cell cell)
    {
        List<Cell> result = new List<Cell>();
        foreach (var dir in DIRS)
        {
            Cell next = FindCell(cell.pos.x + dir.x, cell.pos.y + dir.y);
            if (next != null)
            {
                result.Add(next);
            }
        }
        return result;
    }
}
