using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private float _health; 
    [SerializeField] private float _damage; // Количество здоровья и урона соответственно
    [SerializeField] private Animation _atackAnim; // Анимация атаки
    [SerializeField] private Animation _moveAnimation; // Анимация перемещения
    [SerializeField] private Cell _current; // Клетка, на которой стоит игрок

    public void GoToCell(Cell cell)
    {
        if (cell.type == Cell.CellType.EnemyTeam) // Если клетнка занята вргом
        {
            if (Attack((Enemy)cell.unit)) // Если удалось победить врага
            {
                Move(cell); // Переместиться на клетку
            }
        }
        else if (cell.type == Cell.CellType.Empty) // Если клетка пустая 
        {
            Move(cell); // Переместиться на клетку
        }
        // Если клетка своя, то можно какое-то окно выводить, но пока оставлю пустым
    }

    public void Move(Cell cell)
    {
        _current.type = Cell.CellType.Empty; // Текущая клетка становится пустой
        cell.type = Cell.CellType.AllyTeam; // Изменение статуса занимаемой клетки на "занятый"
        transform.position = new Vector3(cell.transform.position.x, transform.position.y, cell.transform.position.z); 
        _moveAnimation.Play();
    }

    public bool Attack(Enemy enemy)
    {
        enemy._health -= _damage; // Отнимаем здоровье у врага
        _atackAnim.Play();

        if (enemy._health <= 0) // Если здоровье врага <= 0
        {
            enemy.Die();
            return true; // Вызываем метод смерти и возвращаем победу
        }
        return false; // Пока враг не мёртв, возвращаем false
    }

    public void Die()
    {
        Destroy(gameObject); // Пока что смерть просто уничтожает объект
    }
}
