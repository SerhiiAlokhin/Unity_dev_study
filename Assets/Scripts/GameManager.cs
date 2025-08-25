using UnityEngine;
using Dreamteck.Forever;

public class GameManager : MonoBehaviour
{
    public static GameManager I { get; private set; }
    public bool IsGameActive { get; private set; }

    [Header("Refs")]
    [SerializeField] private Runner runner;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Animator animator;

    void Awake() { I = this; StopGame(); }

    public void StartGame()
    {
        IsGameActive = true;
        if (runner) runner.enabled = true;
        if (rb) { rb.isKinematic = false; rb.linearVelocity = Vector3.zero; }
        if (animator) animator.SetBool("IsRunning", true);
    }

    public void StopGame()
    {
        IsGameActive = false;
        if (runner) runner.enabled = false;
        if (rb) { rb.isKinematic = true; rb.linearVelocity = Vector3.zero; }
        if (animator) animator.SetBool("IsRunning", false);
    }
}