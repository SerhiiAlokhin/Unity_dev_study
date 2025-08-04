using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private Transform target;      // сфера
    [SerializeField] private Vector3 offset = new Vector3(0f, 5f, -7f);
    [SerializeField] private float smoothSpeed = 5f;

    private void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.LookAt(target); // камеру направляем на шар
    }
}