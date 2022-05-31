using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public Unit unit; // ����, ������� �������� ������

    public enum CellType // ��������� �������� ��� ����� ��������
    {
        AllyTeam,
        EnemyTeam,
        Empty
    }
    public CellType type { get; set; }
}
