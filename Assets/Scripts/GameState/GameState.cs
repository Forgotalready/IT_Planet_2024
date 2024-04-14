using UnityEngine;

public class GameState : MonoBehaviour
{
    public static GameState Instance { get; private set; }

    [SerializeField] private GameObject _choseObject;

    public GameObject choseObject { 
        get { return _choseObject; }
        set { _choseObject = value; }
    }

    void Start()
    {
        Instance = this;    
    }
}
