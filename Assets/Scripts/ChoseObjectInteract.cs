using System.Collections.Generic;
using UnityEngine;

public class ChoseObjectInteract : MonoBehaviour
{

    [SerializeField] private List<GameObject> _canInteract;

    public void Interact(GameObject interactor)
    {
        if (_canInteract.Contains(interactor))
        {
            Debug.Log("Запуск анимации");
        }
        else {
            Debug.Log("Вывод сообщение:\"Предмет сюда не подходит\"");
        }
    }
}
