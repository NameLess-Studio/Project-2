using UnityEngine;

public class Unit : MonoBehaviour
{
    public float _health; 
    public float _damage; // ���������� �������� � ����� ��������������
    [SerializeField] private Animation _atackAnim; // �������� �����
    [SerializeField] private Animation _moveAnim; // �������� �����������
    [SerializeField] private Animation _dieAnim; // �������� ������
    [SerializeField] private Cell _current; // ������, �� ������� ����� �����

    public void GoToCell(Cell cell)
    {
        if (cell.unit == null) // ���� ������ ������ 
        {
            Move(cell); // ������������� �� ������
        }
        else// ���� ������� ������ �����
        {
            if (Attack(cell.unit)) // ���� ������� �������� �����
            {
                Move(cell); // ������������� �� ������
            }
        }
        // ���� ������ ����, �� ����� �����-�� ���� ��������, �� ���� ������� ������
    }

    public void Move(Cell cell)
    {
        transform.position = new Vector3(cell.transform.position.x, transform.position.y, cell.transform.position.z);  // ��������� ������� 
        _moveAnim.Play(); // �������� ������������
    }

    public bool Attack(Unit unit)
    {
        unit._health -= _damage; // �������� �������� � �����
        _atackAnim.Play();

        if (unit._health <= 0) // ���� �������� ����� <= 0
        {
            unit.Die();
            return true; // �������� ����� ������ � ���������� ������
        }
        return false; // ���� ���� �� ����, ���������� false
    }

    public void Die()
    {
        _dieAnim.Play();
        Destroy(gameObject);
    }
}
