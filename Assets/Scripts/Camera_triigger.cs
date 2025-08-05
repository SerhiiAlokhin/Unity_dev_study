using UnityEngine;
using Cinemachine;

public class CameraTrigger : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera targetCamera;
    [SerializeField] private CinemachineVirtualCamera playerCamera;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SwitchToCamera(targetCamera);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SwitchToCamera(playerCamera);
        }
    }

    private void SwitchToCamera(CinemachineVirtualCamera camToEnable)
    {
        GameObject[] taggedCameras = GameObject.FindGameObjectsWithTag("Camera");

        foreach (GameObject go in taggedCameras)
        {
            CinemachineVirtualCamera cam = go.GetComponent<CinemachineVirtualCamera>();
            if (cam != null)
                cam.Priority = 0;
        }

        if (camToEnable != null)
            camToEnable.Priority = 10;
    }
}