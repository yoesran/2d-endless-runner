using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundControllerAll : MonoBehaviour
{
    public AudioClip boss_step;
    public AudioClip boss_malfunction;
    public AudioClip boss_laser;
    public AudioClip player_hurt;
    private AudioSource audioPlayer;

    private void Start()
    {
        audioPlayer = GetComponent<AudioSource>();
    }

    public void PlayBoss_step()
    {
        audioPlayer.PlayOneShot(boss_step);
    }
    public void PlayBoss_Malfunction()
    {
        audioPlayer.PlayOneShot(boss_malfunction);
    }
    public void PlayBoss_Laser()
    {
        audioPlayer.PlayOneShot(boss_laser);
    }
    public void PlayPayer_Hurt()
    {
        audioPlayer.PlayOneShot(player_hurt);
    }




    public void stop()
    {
        audioPlayer.Stop();
    }
    
}