using JSAM;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public sealed class SettingsSave : MonoBehaviour
{

    [Header("Sound UI Elements")]
    [SerializeField] private TMP_Text _soundsText;
    [SerializeField] private TMP_Text _musicVText;

    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _soundsSlider;


    private void OnEnable()
    {
        AudioManager.OnAudioManagerInitialized += ApplyToJsamFromUI;
    }
    private void OnDisable()
    {
        AudioManager.OnAudioManagerInitialized -= ApplyToJsamFromUI;
    }

    private void Start()
    {

        float sound = PlayerPrefs.GetFloat(Constants.SoundsVolumeKey, Constants.DefaultSoundVolume);   // 0..100
        float music = PlayerPrefs.GetFloat(Constants.MusicVolumeKey, Constants.DefaultMusicVolume);   // 0..100

        _soundsSlider.SetValueWithoutNotify(sound);
        _musicSlider.SetValueWithoutNotify(music);

        _soundsText.text = _soundsSlider.value.ToString("0");
        _musicVText.text = _musicSlider.value.ToString("0");

        ApplyToJsam(music, sound);

        _soundsSlider.onValueChanged.AddListener(OnSoundsSliderChanged);
        _musicSlider.onValueChanged.AddListener(OnMusicSliderChanged);
    }

    public void ResetSettings()
    {
        PlayerPrefs.SetFloat(Constants.SoundsVolumeKey, Constants.DefaultSoundVolume);
        PlayerPrefs.SetFloat(Constants.MusicVolumeKey, Constants.DefaultMusicVolume);
    }

    public void OnMusicSliderChanged(float value)
    {
        _musicVText.text = value.ToString("0");
        PlayerPrefs.SetFloat(Constants.MusicVolumeKey, value);
        PlayerPrefs.Save();
        AudioManager.MusicVolume = Mathf.Clamp01(value / 100f);
    }

    public void OnSoundsSliderChanged(float value)
    {
        _soundsText.text = value.ToString("0");
        PlayerPrefs.SetFloat(Constants.SoundsVolumeKey, value);
        PlayerPrefs.Save();
        AudioManager.SoundVolume = Mathf.Clamp01(value / 100f);
    }

    private void ApplyToJsamFromUI()
    {
        ApplyToJsam(_musicSlider.value, _soundsSlider.value);
    }

    private static void ApplyToJsam(float music0_100, float sfx0_100)
    {
        AudioManager.MusicVolume = Mathf.Clamp01(music0_100 / 100f);
        AudioManager.SoundVolume = Mathf.Clamp01(sfx0_100 / 100f);
    }
}