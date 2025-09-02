using UnityEngine;
using JSAM;

public class StartMenuMusic : MonoBehaviour
{
    [SerializeField] private MusicFileObject music;
    private static bool started;

    private void OnEnable()
    {
        if (started || music == null) return;

        //Debug.Log($"[SFX] SoundVol={AudioManager.SoundVolume}, MasterVol={AudioManager.MasterVolume}");

        float soundvol = PlayerPrefs.GetFloat(Constants.SoundsVolumeKey, Constants.DefaultSoundVolume);
        float musicVol = PlayerPrefs.GetFloat(Constants.MusicVolumeKey, Constants.DefaultMusicVolume);
        AudioManager.SoundVolume = soundvol / 100f;
        AudioManager.MusicVolume = musicVol / 100f;

        started = true;
        AudioManager.PlayMusic(music, true); 
    }
}