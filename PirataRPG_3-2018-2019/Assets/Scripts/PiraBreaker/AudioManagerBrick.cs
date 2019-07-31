using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerBrick : MonoBehaviour
{
    public AudioSource BGMusic;

    public void PlayBGMusic()
    {
        BGMusic.Play();
    }

}
