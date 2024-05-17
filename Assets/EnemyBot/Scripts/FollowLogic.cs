using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Класс, который заставляет спрайт персонажа двигаться
/// за его логической частью.
/// 
/// <remark>
/// Невероятно важный класс, обязательно к изучению.
/// </remark>
/// 
/// </summary>
public class FollowLogic : MonoBehaviour
{
    /// <summary>
    /// Объект логики.
    /// </summary>    
    [SerializeField] private GameObject _enemyLogic;

    private void Update()
    {
        transform.position = _enemyLogic.transform.position;
    }
}
