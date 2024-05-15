using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private State startingState;
    [SerializeField] private float roamingDistanceMax = 7f;
    [SerializeField] private float roamingDistanceMin = 3f;
    [SerializeField] private float roamingTimerMax = 2f;


    private NavMeshAgent _navMeshAgent;
    private State _state;
    private float _roamingTime;
    private Vector3 _roamPosition;
    private Vector3 _startPosition;
    private enum State { 
        Idle,
        Roaming
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
        switch (_state)
        {
            default:
            case State.Idle:
                break;
            case State.Roaming:
                _roamingTime -= Time.deltaTime;
                if (_roamingTime < 0) {
                    Roaming();
                    _roamingTime = roamingTimerMax;
                }
                break;
        }
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
}
