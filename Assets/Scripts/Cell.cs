using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public Unit unit; // Юнит, который занимает клетку

    public enum CellType // Временная болванка для моего удобства
    {
        AllyTeam,
        EnemyTeam,
        Empty
    }
    public CellType type { get; set; }
}
