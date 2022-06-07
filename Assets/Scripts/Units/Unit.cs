using UnityEngine;

public class Unit : MonoBehaviour
{
    public float _health; 
    public float _damage; // Количество здоровья и урона соответственно
    [SerializeField] private Animation _atackAnim; // Анимация атаки
    [SerializeField] private Animation _moveAnim; // Анимация перемещения
    [SerializeField] private Animation _dieAnim; // Анимация смерти
    [SerializeField] private Cell _current; // Клетка, на которой стоит игрок

    public void GoToCell(Cell cell)
    {
        if (cell.unit == null) // Если клетка пустая 
        {
            Move(cell); // Переместиться на клетку
        }
        else// Если клетнка занята вргом
        {
            if (Attack(cell.unit)) // Если удалось победить врага
            {
                Move(cell); // Переместиться на клетку
            }
        }
        // Если клетка своя, то можно какое-то окно выводить, но пока оставлю пустым
    }

    public void Move(Cell cell)
    {
        transform.position = new Vector3(cell.transform.position.x, transform.position.y, cell.transform.position.z);  // Изменение позиции 
        _moveAnim.Play(); // Анимация передвиджния
    }

    public bool Attack(Unit unit)
    {
        unit._health -= _damage; // Отнимаем здоровье у врага
        _atackAnim.Play();

        if (unit._health <= 0) // Если здоровье врага <= 0
        {
            unit.Die();
            return true; // Вызываем метод смерти и возвращаем победу
        }
        return false; // Пока враг не мёртв, возвращаем false
    }

    public void Die()
    {
        _dieAnim.Play();
        Destroy(gameObject);
    }
}
