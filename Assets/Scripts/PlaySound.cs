using UnityEngine;
using JSAM;
using System.Threading.Tasks;


public class PlaySound : MonoBehaviour
{
    private async void Start()
    {
        await Task.Delay(1000);

        //AudioManager.PlaySound(myAudioLibSounds.RingSFX);
        var music = AudioManager.PlayMusic(myAudioLibMusic.MusicFX, true);
    }
}
