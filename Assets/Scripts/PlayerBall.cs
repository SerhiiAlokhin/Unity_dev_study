using UnityEngine;
using Cinemachine;

public class PlayerBall : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private Rigidbody rb;
    private CinemachineBrain brain;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        brain = Camera.main.GetComponent<CinemachineBrain>();
    }

    private void Update()
    {
        if (brain == null || brain.ActiveVirtualCamera == null)
            return;

        Transform camTransform = brain.OutputCamera.transform;

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 camForward = camTransform.forward;
        Vector3 camRight = camTransform.right;

        camForward.y = 0f;
        camRight.y = 0f;

        camForward.Normalize();
        camRight.Normalize();

        Vector3 moveDir = camForward * moveZ + camRight * moveX;
        rb.AddForce(moveDir * moveSpeed);
    }
}