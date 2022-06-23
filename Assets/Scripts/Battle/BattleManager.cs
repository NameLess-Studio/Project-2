using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [SerializeField]
    private Ally currentUnit; //Действующий юнит

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            print("Click!");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] hits = Physics.RaycastAll(ray, 100);
            foreach (RaycastHit hit in hits)
            {
                print(hit.transform);
                if (hit.transform.tag == "Level")
                {
                    currentUnit.GoToCell(hit.transform.GetComponent<Cell>());
                }
            }
        }
    }

    /// <summary>
    /// Обработочик нажатия на клетку
    /// </summary>
    /// <param name="cell"></param>
    private void Action(Cell cell)
    {
        if (cell.unit == null)
            currentUnit.GoToCell(cell);
        else
            currentUnit.Attack(cell.unit);
    }
}
