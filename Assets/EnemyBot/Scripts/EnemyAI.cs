using System;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Класс описывающий логику движения NPC
/// </summary>

public class EnemyAI : MonoBehaviour
{
    /// <summary>
    /// Начальное состояние.
    /// </summary>
    [SerializeField] private State startingState;

    /// <summary>
    /// Объект игрока
    /// </summary>
    [SerializeField] private GameObject Player;

    /// <summary>
    /// Максимальная удалённость точки, нового положения.
    /// </summary>
    [SerializeField] private float roamingDistanceMax = 7f;

    /// <summary>
    /// Минимальная удалённость точки, нового положения.
    /// </summary>
    [SerializeField] private float roamingDistanceMin = 3f;

    /// <summary>
    /// Максимальное время перемещения.
    /// </summary>
    [SerializeField] private float roamingTimerMax = 2f;

    /// <summary>
    /// Максимальное время простоя.
    /// </summary>
    [SerializeField] private float idleTimeMax = 2f;

    /// <summary>
    /// Расстояние, при котором NPC начинают догонят игрока.
    /// </summary>
    [SerializeField] private float howNear = 50f;


    private NavMeshAgent _navMeshAgent;

    private State _state;

    private float _roamingTime;
    private float _idleTime;

    private Vector3 _roamPosition;
    private Vector3 _startPosition;


    /// <summary>
    /// Возможные состояния NPC.
    /// </summary>
    private enum State { 
        Idle,
        Roaming,
        PlayerNear
    }

    private void Start() { 
        _startPosition = transform.position;
    }

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _state = startingState;
    }

    private void Update()
    {
        System.Random rand = new System.Random();

        if (isPlayerNear(howNear)) { 
            _state = State.PlayerNear;
        }
        switch (_state)
        {
            default:
            case State.Idle:
                _idleTime -= Time.deltaTime;
                if (_idleTime < 0)
                {
                    _idleTime = idleTimeMax;
                    if (rand.Next(0, 2) == 1)
                        _state = State.Roaming;
                }
                break;
            case State.Roaming:
                _roamingTime -= Time.deltaTime;
                if (_roamingTime < 0) {
                    Roaming();
                    _roamingTime = roamingTimerMax;
                    if (rand.Next(0, 2) == 1)
                        _state = State.Idle;
                }
                break;
            case State.PlayerNear:
                chasingPlayer();
                break;
        }
    }

    private void chasingPlayer()
    {
        _navMeshAgent.SetDestination(Player.transform.position);
    }

    private void Roaming()
    {
        _roamPosition = GetRoamingPosition();
        _navMeshAgent.SetDestination(_roamPosition);
    }

    private Vector3 GetRoamingPosition()
    {
        return _startPosition + GetRandomDir() * UnityEngine.Random.Range(roamingDistanceMin, roamingDistanceMax);
    }

    private Vector3 GetRandomDir()
    {
        return new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)).normalized;
    }

    private bool isPlayerNear(float howNear) {
        return (Vector3.Distance(Player.transform.position, transform.position) < howNear);
    }
}
