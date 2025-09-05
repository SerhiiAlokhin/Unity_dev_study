using UnityEngine;

public class Trigger_gate : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger in: " + other.name);
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Trigger out: " + other.name);
    }
}
