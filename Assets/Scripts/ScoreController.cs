using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    CharacterMoveController Player;
    SoundControllerAll sound_controller_all;
    public GameObject boss;
    public GameObject boss_health;
    private int currentScore = 0;
    private int scoreboss;
    public int score_muncul_boss;
    

    private void Start()
    {
        // reset
        sound_controller_all = FindObjectOfType<SoundControllerAll>();
        Player = FindObjectOfType<CharacterMoveController>();
        currentScore = 0;
    }
    private void Update()
    {
        if (scoreboss < score_muncul_boss)
        {
            scoreboss = currentScore;
        }
        if (scoreboss == score_muncul_boss)
        {
            sound_controller_all.PlayBoss_step();
            Instantiate(boss, new Vector3(Player.transform.position.x+25, -2.730872f, 0f), Quaternion.identity);
            boss_health.SetActive(true);
            scoreboss = score_muncul_boss+1;
        }
            
    }

    public float GetCurrentScore()
    {
        return currentScore;
    }

    public void IncreaseCurrentScore(int increment)
    {
        currentScore += increment;
    }
}
