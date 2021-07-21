using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Система построения маршрутов. Скрипт применяется к управляяемому объекту.
В сцене присутствует специальный объект, стостоящий из группы и вложеных целей.
Грппа подлючается к этому скрипту. Вложенные цели имеют строгое именование 1, 2, 3 и т.д.
Управляемый объект следует по этим целям попорядку.*/
public class routeSystemV1 : MonoBehaviour
{
    public GameObject routeSystem;         // ссылка на систему маршрута (набор целей)
    private GameObject currentTarget;      // для хранения объета текущей цели
    public int currentTargetIndex = 1;     // индекс(имя) текущей цели
    public float speed = 1f;               // скорость движения к цели
    public float speedRot = 1f;            // скорость поворота
    int sumTarget;                         // количество целей в системе (посути имя последней цели)
    string routeSystemName;                // имя назначенной системы маршрута
    string findText;                       // текст запроса поиска
    public float contactDistance = 0.001f; // растояние, условия достижения цели

    // При старте Юнити.
    void Start()
    {
        foreach (Transform child in routeSystem.transform) sumTarget++; // подсчитываем кол-во целей
        routeSystemName = routeSystem.name;                             // создаём строку и именем системы маршрутов
        FindCurretnTarget();
    }

    // В каждом кадре Юнити.
    void Update()
    {
        MoveTarget();
        checkPosition();
    }

    // Перемещение объекта к целям
    void MoveTarget()
    {
        // смотреть на цель плавно
        Quaternion localRotation = Quaternion.LookRotation(currentTarget.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, localRotation, speedRot * Time.deltaTime);

        // следование к цели
        transform.position = Vector3.MoveTowards(transform.position, 
            currentTarget.transform.position, speed * Time.deltaTime);
    }

    // Ищем текущую цель в сцене.
    void FindCurretnTarget()
    {
        findText = routeSystemName + "/" + currentTargetIndex.ToString(); // формирование строки поиска
        currentTarget = GameObject.Find(findText);
    }

    // проверяем позицию перемещаемого объекта относительно целей
    void checkPosition()
    {
        // если доехали до цели
        if (Vector3.Distance(transform.position, currentTarget.transform.position) < contactDistance)
        {
            Debug.Log("приехали");
            if (currentTargetIndex == sumTarget)currentTargetIndex = 1; // если доехали до последней цели то сначала
            else currentTargetIndex ++;                                 // иначе выбираем следующую цель
            FindCurretnTarget();                                        // новую выбранную цель ищем
        }
    }
}