using UnityEngine;
using System.Collections.Generic;


public class GameState : MonoBehaviour
{
    public static GameState Instance { get; private set; }

    [SerializeField] private GameObject _choseObject;
    [SerializeField] private List<Item> _item;

    public List<Item> Item { 
        get { return _item; }
        private set {}
    }
    public GameObject choseObject { 
        get { return _choseObject; }
        set { _choseObject = value; }
    }

    void Start()
    {
        Instance = this;    
    }
}
