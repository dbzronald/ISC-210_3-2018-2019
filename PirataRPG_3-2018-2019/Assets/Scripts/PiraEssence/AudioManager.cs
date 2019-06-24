using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource EssenceFX;

    public AudioSource SpikeHit;

    public AudioSource BGMusic;

    public AudioSource BoatExploded;

    public void PlayEsssenceFX()
    {
        EssenceFX.Play();
    }

    public void PlaySpikeHit()
    {
        SpikeHit.Play();
    }

    public void BoatExplodedFX()
    {
        BoatExploded.Play();
    }

    public void PlayBGMusic()
    {
        BGMusic.Play();
    }
}
