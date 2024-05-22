using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> manualCards;

    private int manualState = 0;

    private void Start()
    {
        manualCards[manualState].SetActive(true);
    }

    void Update()
    {
        if (manualState == manualCards.Count) return;
        ICard card = manualCards[manualState].GetComponent<ICard>();

        if (card.userLogic()) {
            manualState += 1;
            manualCards[manualState - 1].SetActive(false);
            if(manualState != manualCards.Count)
                manualCards[manualState].SetActive(true);
        }
    }
}
