using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    [SerializeField] private TextMeshProUGUI ringsText;
    private RingsData data;

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        data = GameSaver.TryLoad<RingsData>(out var loaded) ? loaded : new RingsData();
        //Debug.Log($"[Rings] loaded total={data.totalRings} (from file)");
        UpdateUI();
    }

    public void AddRing(int amount = 1)
    {
        data.totalRings += amount;
        GameSaver.Save(data);
        //Debug.Log($"[Rings] +{amount} => {data.totalRings} (saved)");
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (ringsText) ringsText.text = "x" + data.totalRings;
    }
}