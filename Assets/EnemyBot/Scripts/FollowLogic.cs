using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// �����, ������� ���������� ������ ��������� ���������
/// �� ��� ���������� ������.
/// 
/// <remark>
/// ���������� ������ �����, ����������� � ��������.
/// </remark>
/// 
/// </summary>
public class FollowLogic : MonoBehaviour
{
    /// <summary>
    /// ������ ������.
    /// </summary>    
    [SerializeField] private GameObject _enemyLogic;

    private void Update()
    {
        transform.position = _enemyLogic.transform.position;
    }
}
