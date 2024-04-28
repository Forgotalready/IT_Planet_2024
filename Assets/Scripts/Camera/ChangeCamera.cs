using UnityEngine;
using Cinemachine;

public class ChangeCamera : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera cam;
    [SerializeField] private CinemachineVirtualCamera mainCam;
    [SerializeField] private GameObject _player;

    private bool isMoved = false;

    void Update()
    {

        if (PlayerInputHandler.Instance.rotateRightTriggered)//(Input.GetKeyDown(KeyCode.R))
        {
            if (!isMoved)
            {
                cam.Priority = 10;
                mainCam.Priority = 9;
                isMoved = true;
                cam.Follow = _player.transform;
            }
            else
            {
                cam.Priority = 9;
                mainCam.Priority = 10;
                isMoved = false;
            }
        }
    }
}
