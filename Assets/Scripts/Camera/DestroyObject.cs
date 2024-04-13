using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    private bool isHitted = false;
    private GameObject hitObject;
    public void checkHit() {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if (!isHitted)
            {
                Debug.Log("This work");
                isHitted = true;
                hitObject = hit.collider.gameObject;
                hitObject.SetActive(false);
            }
            else {
                isHitted = false;
                hitObject.SetActive(true);
            }
        }
    }
}
