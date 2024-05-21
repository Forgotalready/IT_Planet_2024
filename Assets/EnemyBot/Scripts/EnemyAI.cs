using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



/// <summary>
/// Класс описывающий логику движения NPC
/// </summary>

public class EnemyAI : MonoBehaviour
{

    /// <summary>
    /// Путь движения ИИ
    /// </summary>
    [SerializeField] private List<GameObject> track;

    /// <summary>
    /// Предметы, в которых можно прятаться
    /// </summary>
    private GameObject[] _hideObjects;

    /// <summary>
    /// Предметы еды
    /// </summary>
    private GameObject[] _foodObjects;

    /// <summary>
    /// Начальное состояние.
    /// </summary>
    [SerializeField] private State startingState;

    /// <summary>
    /// Объект игрока
    /// </summary>
    [SerializeField] private GameObject Player;


    /// <summary>
    /// Расстояние, при котором NPC начинают догонят игрока.
    /// </summary>
    [SerializeField] private float howNear = 10f;

    /// <summary>
    /// Нормальная скорость бота
    /// </summary>
    [SerializeField] private float normalSpeed = 1.2f;

    /// <summary>
    /// Скорость, когда бот гонится за игроком
    /// </summary>
    [SerializeField] private float chasingSpeed = 2.0f;

    private NavMeshAgent _navMeshAgent;

    private State _state;

    private float _roamingTime;
    private float _idleTime;

    private Vector3 _roamPosition;
    private Vector3 _startPosition;
    private Animator _animator;

    private bool _isPlayerHide = false;

    private int _posNumber = 0;


    /// <summary>
    /// Возможные состояния NPC.
    /// </summary>
    private enum State { 
        Roaming,
        PlayerNear,
        FoodDetect
    }

    private void Start() { 
        _startPosition = transform.position;
    }

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.speed = normalSpeed;

        _hideObjects = GameObject.FindGameObjectsWithTag("CameraChangingObject");

        Transform thisTransform = GetComponent<Transform>();

        foreach (Transform child in thisTransform)
        {
            GameObject childObject = child.gameObject;
            Animator childAnimator = childObject.GetComponent<Animator>();
            if (childAnimator != null)
            {
                _animator = childAnimator;
                break;
            }
        }

        foreach (GameObject go in _hideObjects)
        {
            HideObject ho = go.GetComponent<HideObject>();
            if(ho != null)
                ho.playerHide += Hide;
        }
        _navMeshAgent.SetDestination(track[_posNumber].transform.position);
        _state = startingState;
    }

    private void Hide()
    {
        if (_state == State.PlayerNear) {
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #elif UNITY_STANDALONE
                Application.Quit(); 
            #endif
        }
        _isPlayerHide = true;
    }

    private void Update()
    {
        _foodObjects = GameObject.FindGameObjectsWithTag("Food");

        GameObject nearFood = isFoodNear(howNear);

        if (!(nearFood == null))
            _state = State.FoodDetect;

        else if (isPlayerNear(howNear) && !_isPlayerHide)
        {
            _state = State.PlayerNear;
            _navMeshAgent.speed = chasingSpeed;
        }
        _animator.SetFloat("Velocity", _navMeshAgent.speed / chasingSpeed);
        switch (_state)
        {
            default:
            case State.Roaming:
                roamingStateMovement();
                break;
            case State.PlayerNear:
                chasingPlayer();
                break;
            case State.FoodDetect:
                foodDetectBehv(nearFood);
                break;
        }
    }

    private void foodDetectBehv(GameObject nearFood)
    {
         _navMeshAgent.SetDestination(nearFood.transform.position);
    }
    /// <summary>
    /// Метод, который проверяет нет ли рядом с ботом еды
    /// </summary>
    /// <param name="howNear">Насколько близко должна находится еда</param>
    /// <returns>Ближающую еду или null</returns>
    private GameObject isFoodNear(float howNear)
    {
        GameObject result = null;
        foreach (GameObject go in _foodObjects)
            if (Vector3.Distance(go.transform.position, transform.position) < howNear)
            {
                if (result == null)
                    result = go;
                else
                    if (Vector3.Distance(go.transform.position, transform.position) < Vector3.Distance(result.transform.position, transform.position))
                        result = go;
            }
        return result;       
    }

    /// <summary>
    /// Логика действий в состояние roaming
    /// </summary>
    private void roamingStateMovement()
    {
        if (Vector3.Distance(track[_posNumber].transform.position, transform.position) < 1.5f){
            _posNumber = (_posNumber + 1) % track.Count;
            _navMeshAgent.SetDestination(track[_posNumber].transform.position);
        }
    }
    private void chasingPlayer()
    {
        _navMeshAgent.SetDestination(Player.transform.position);
    }

    private bool isPlayerNear(float howNear) {
        return (Vector3.Distance(Player.transform.position, transform.position) < howNear);
    }
}
