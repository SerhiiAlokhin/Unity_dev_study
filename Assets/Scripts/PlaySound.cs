using UnityEngine;
using JSAM;

public class StartMenuMusic : MonoBehaviour
{
    [SerializeField] private MusicFileObject music;
    private static bool started;

    private void OnEnable()
    {
        if (started || music == null) return;
        started = true;
        AudioManager.PlayMusic(music, true); 
    }
}