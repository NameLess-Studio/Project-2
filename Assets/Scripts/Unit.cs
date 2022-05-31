using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private float _health; 
    [SerializeField] private float _damage; // ���������� �������� � ����� ��������������
    [SerializeField] private Animation _atackAnim; // �������� �����
    [SerializeField] private Animation _moveAnimation; // �������� �����������
    [SerializeField] private Cell _current; // ������, �� ������� ����� �����

    public void GoToCell(Cell cell)
    {
        if (cell.type == Cell.CellType.EnemyTeam) // ���� ������� ������ �����
        {
            if (Attack((Enemy)cell.unit)) // ���� ������� �������� �����
            {
                Move(cell); // ������������� �� ������
            }
        }
        else if (cell.type == Cell.CellType.Empty) // ���� ������ ������ 
        {
            Move(cell); // ������������� �� ������
        }
        // ���� ������ ����, �� ����� �����-�� ���� ��������, �� ���� ������� ������
    }

    public void Move(Cell cell)
    {
        _current.type = Cell.CellType.Empty; // ������� ������ ���������� ������
        cell.type = Cell.CellType.AllyTeam; // ��������� ������� ���������� ������ �� "�������"
        transform.position = new Vector3(cell.transform.position.x, transform.position.y, cell.transform.position.z); 
        _moveAnimation.Play();
    }

    public bool Attack(Enemy enemy)
    {
        enemy._health -= _damage; // �������� �������� � �����
        _atackAnim.Play();

        if (enemy._health <= 0) // ���� �������� ����� <= 0
        {
            enemy.Die();
            return true; // �������� ����� ������ � ���������� ������
        }
        return false; // ���� ���� �� ����, ���������� false
    }

    public void Die()
    {
        Destroy(gameObject); // ���� ��� ������ ������ ���������� ������
    }
}
