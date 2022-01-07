using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSoundController : MonoBehaviour
{
    public AudioClip jump;
    public AudioClip scoreHighlight;
    public AudioClip coin;
    public AudioClip shoot;
    public AudioClip spark;
    public AudioClip basic;
    public AudioClip EnemyFlyDestroy;
    public AudioClip EnemyGroundDestroy;
    public AudioClip PlayerDestroy;
    public AudioClip StartSound;
    private AudioSource audioPlayer;

    private void Start()
    {
        audioPlayer = GetComponent<AudioSource>();
    }

    public void PlayJump()
    {
        audioPlayer.PlayOneShot(jump);
    }

    public void PlayScoreHighlight()
    {
        audioPlayer.PlayOneShot(scoreHighlight);
    }
    public void PlayCoin()
    {
        audioPlayer.PlayOneShot(coin);
    }
    public void PlayShoot()
    {
        audioPlayer.PlayOneShot(shoot);
    }
    public void PlaySpark()
    {
        audioPlayer.PlayOneShot(spark);
    }
    public void PlayBasic()
    {
        audioPlayer.PlayOneShot(basic);
    }
    public void PlayEnemyFlyDestroy()
    {
        audioPlayer.PlayOneShot(EnemyFlyDestroy);
    }
    public void PlayEnemyGroundDestroy()
    {
        audioPlayer.PlayOneShot(EnemyGroundDestroy);
    }
    public void PlayPlayerDestroy()
    {
        audioPlayer.PlayOneShot(PlayerDestroy);
    }
    public void PlayStartSound()
    {
        audioPlayer.PlayOneShot(StartSound);
    }
}
