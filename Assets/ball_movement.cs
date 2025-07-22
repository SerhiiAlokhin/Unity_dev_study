using UnityEngine;

public class PlayerBall : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");   

        Vector3 movement = new Vector3(moveX, 0f, moveZ);
        rb.AddForce(movement * moveSpeed);
    }
}