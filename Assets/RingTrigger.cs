using UnityEngine;

public class RingTrigger : MonoBehaviour
{
    [SerializeField] private string playerTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            ScoreManager.Instance?.AddRing(1);
        }
    }
}