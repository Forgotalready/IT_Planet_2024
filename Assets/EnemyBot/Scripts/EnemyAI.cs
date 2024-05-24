using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// ����� ����������� ��������� ����.
/// </summary>
public class EnemyAnimation {
    private Animator _animator;

    private EnemyAI _enemyAI;
    public EnemyAnimation(EnemyAI ai) { 
        _enemyAI = ai;
    }

    /// <summary>
    /// ��������� ��������.
    /// ����� ���������� �������� � �������.
    /// </summary>
    public void setupAnimation() {
        Transform thisTransform = _enemyAI.GetComponent<Transform>();

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
    }
    /// <summary>
    /// ��������� �������� ��������
    /// </summary>
    /// <param name="enemySpeed">������� �������� NPC</param>
    /// <param name="maxSpeed">������������ �������� NPC</param>
    public void setAnimationSpeed(float enemySpeed, float maxSpeed)  => _animator.SetFloat("Velocity", enemySpeed / maxSpeed);

    public void setEating(bool isEating) => _animator.SetBool("Eats", isEating);
    
}

/// <summary>
/// ����� ����������� ������ �������� NPC
/// </summary>

public class EnemyAI : MonoBehaviour
{

    /// <summary>
    /// ���� �������� ��
    /// </summary>
    [SerializeField] private List<GameObject> track;

    /// <summary>
    /// ��������, � ������� ����� ���������
    /// </summary>
    private GameObject[] _hideObjects;

    /// <summary>
    /// �������� ���
    /// </summary>
    private GameObject[] _foodObjects;

    /// <summary>
    /// ��������� ���������.
    /// </summary>
    [SerializeField] private State startingState;

    /// <summary>
    /// ������ ������
    /// </summary>
    [SerializeField] private GameObject Player;


    /// <summary>
    /// ����������, ��� ������� NPC �������� ������� ������.
    /// </summary>
    [SerializeField] private float howNear = 10f;

    /// <summary>
    /// ���������� �������� ����
    /// </summary>
    [SerializeField] private float normalSpeed = 1.2f;

    /// <summary>
    /// ��������, ����� ��� ������� �� �������
    /// </summary>
    [SerializeField] private float chasingSpeed = 2.0f;

    /// <summary>
    /// ������������ ����� �������� ���.
    /// </summary>
    [SerializeField] private float maxEatingTime = 5.0f;

    private NavMeshAgent _navMeshAgent;

    private float eatingTime;

    private State _state;

    private EnemyAnimation _enemyAnimation;

    private bool _isPlayerHide = false;

    private int _posNumber = 0;


    /// <summary>
    /// ��������� ��������� NPC.
    /// </summary>
    private enum State
    {
        Roaming,
        PlayerNear,
        FoodDetect
    }

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.speed = normalSpeed;

        eatingTime = maxEatingTime;

        _hideObjects = GameObject.FindGameObjectsWithTag("CameraChangingObject");

        _enemyAnimation = new EnemyAnimation(this);
        _enemyAnimation.setupAnimation();

        foreach (GameObject go in _hideObjects)
        {
            Hide ho = go.GetComponent<Hide>();
            if (ho != null)
            {
                Debug.Log("Object assign");
                ho.playerHide += Hide;
            }
        }
        _navMeshAgent.SetDestination(track[_posNumber].transform.position);
        _state = startingState;
    }

    private void Hide()
    {
        if (_state == State.PlayerNear)
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE
                Application.Quit(); 
#endif
        }
        _isPlayerHide = !_isPlayerHide;
        Debug.Log(_isPlayerHide);
    }

    private void Update()
    {
        _foodObjects = GameObject.FindGameObjectsWithTag("Food");
        GameObject nearFood = isFoodNear(howNear);
        //Debug.Log(_isPlayerHide);
        if (!(nearFood == null))
            _state = State.FoodDetect;
        else if (isPlayerNear(howNear) && !_isPlayerHide)
        {
            _state = State.PlayerNear;
            _navMeshAgent.speed = chasingSpeed;
        }
        _enemyAnimation.setAnimationSpeed(_navMeshAgent.speed, chasingSpeed);

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
    /// <summary>
    /// ��������� ����, ���� �� ����� ����� ���.
    /// </summary>
    /// <param name="nearFood">��������� ���</param>
    private void foodDetectBehv(GameObject nearFood)
    {
        _navMeshAgent.SetDestination(nearFood.transform.position);
        if (Vector3.Distance(nearFood.transform.position, transform.position) < 1.5f)
        {

            nearFood.GetComponentInChildren<MeshRenderer>().enabled = false;

            _enemyAnimation.setEating(true);
            if (eatingTime < 0)
            {
                _state = State.Roaming;
                eatingTime = maxEatingTime;
                Destroy(nearFood);
                _enemyAnimation.setEating(false);
            }
            else
                eatingTime -= Time.deltaTime;
        }

    }
    /// <summary>
    /// �����, ������� ��������� ��� �� ����� � ����� ���
    /// </summary>
    /// <param name="howNear">��������� ������ ������ ��������� ���</param>
    /// <returns>��������� ��� ��� null</returns>
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
    /// ������ �������� � ��������� roaming
    /// </summary>
    private void roamingStateMovement()
    {
        _navMeshAgent.SetDestination(track[_posNumber].transform.position);
        if (Vector3.Distance(track[_posNumber].transform.position, transform.position) < 1.5f)
        {
            _posNumber = (_posNumber + 1) % track.Count;
            _navMeshAgent.SetDestination(track[_posNumber].transform.position);
        }
    }
    /// <summary>
    /// �����, ������� ���������� ���� �������� ������.
    /// </summary>
    private void chasingPlayer()  {
        _navMeshAgent.SetDestination(Player.transform.position);
        if (Vector3.Distance(Player.transform.position, transform.position) < 1.5f) {
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #elif UNITY_STANDALONE
                Application.Quit(); 
            #endif
        }
    }

    /// <summary>
    /// ����� ����������� ���� �� ����� �����.
    /// </summary>
    /// <param name="howNear">������� ��������</param>
    /// <returns>������ �� �����</returns>
    /// 
    private bool isPlayerNear(float howNear)
    {
        RaycastHit hit;
        if (Physics.BoxCast(transform.position, transform.localScale * 0.5f, transform.forward, out hit))
        {
            if (hit.collider.gameObject.tag == "Player") return true;
        }
        return false;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Draw a Ray forward from GameObject toward the maximum distance
        Gizmos.DrawRay(transform.position, transform.forward * howNear);
        //Draw a cube at the maximum distance
        Gizmos.DrawWireCube(transform.position + transform.forward * howNear, transform.localScale);
    }
}
