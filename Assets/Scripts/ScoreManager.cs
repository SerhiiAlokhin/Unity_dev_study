using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI ringsText;
    private int rings;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        UpdateUI();
    }

    public void AddRing(int amount = 1)
    {
        rings += amount;
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (ringsText) ringsText.text = "x" + rings.ToString();
    }
}