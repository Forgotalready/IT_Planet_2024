using UnityEngine;

public class ThrowFood : MonoBehaviour
{
    [SerializeField] private GameObject food;
    [SerializeField] private GameObject cameraObj;

    private float _distance = 1.1f;
    private float _floarPosition = 0.2f;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            GameObject instFood = Instantiate(food);
            if (transform.position.magnitude > cameraObj.transform.position.magnitude) {
                _distance = 2.0f - _distance;
            }
            instFood.transform.position = new Vector3(_distance * transform.position.x, _floarPosition, _distance * transform.position.z);
            
        }
    }
}
