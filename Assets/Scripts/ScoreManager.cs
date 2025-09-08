using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI ringsText;
    [SerializeField] private int autosaveEveryRings = 10;


    private RingsData data;
    private int unsavedDelta;   
    private bool valueChanged;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        data = GameSaver.TryLoad<RingsData>(out var loaded) ? loaded : new RingsData();
        UpdateUI();
    }

    public void AddRing(int amount = 1)
    {
        data.totalRings += amount;
        valueChanged = true;
        unsavedDelta += amount;

        if (autosaveEveryRings > 0 && unsavedDelta >= autosaveEveryRings)
            SaveNow();

        UpdateUI();
    }

    public void SaveNow()
    {
        if (!valueChanged) return;
        GameSaver.Save(data);
        valueChanged = false;
        unsavedDelta = 0;
    }

    private void UpdateUI()
    {
        if (ringsText) ringsText.text = "x" + data.totalRings;
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            SaveNow();
        }
    }

    private void OnApplicationQuit()
    {
        SaveNow();
    }
}